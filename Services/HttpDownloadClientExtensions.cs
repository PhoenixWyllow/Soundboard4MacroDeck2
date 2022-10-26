using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soundboard4MacroDeck.Services;

public static class HttpDownloadClientExtensions
{
    public static async Task<byte[]> DownloadBytesAsync(this HttpClient client, string requestUri, IProgress<float> progress, CancellationToken cancellationToken = default)
    {
        // Get the http headers first to examine the content length
        using var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        ThrowForNullResponse(response);
        response.EnsureSuccessStatusCode();
        
        var contentLength = response.Content.Headers.ContentLength;

        // Ignore progress reporting when no progress reporter was 
        // passed or when the content length is unknown
        if (progress is null || !contentLength.HasValue) 
        {
            return await client.GetByteArrayAsync(requestUri, cancellationToken).ConfigureAwait(false);
        }
        
        using MemoryStream destination = new ((int)contentLength);
        using var download = await response.Content.ReadAsStreamAsync(cancellationToken);
        
        // Convert absolute progress (bytes downloaded) into relative progress (0% - 100%)
        Progress<long> relativeProgress = new (totalBytes => progress.Report((float)totalBytes / contentLength.Value));
        
        // Use extension method to report progress while downloading
        await download.CopyToAsync(destination, 8192, relativeProgress, cancellationToken).ConfigureAwait(false);
        progress.Report(1);

        return destination.Length == 0 ? Array.Empty<byte>() : destination.GetBuffer();
    }
    
    public static async Task CopyToAsync(this Stream source, Stream destination, int bufferSize, IProgress<long> progress = null, CancellationToken cancellationToken = default) 
    {
        ValidateArguments(source, destination, bufferSize);

        var buffer = new byte[bufferSize];
        long totalBytesRead = 0;
        int bytesRead;
        while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0) 
        {
            await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
            totalBytesRead += bytesRead;
            progress?.Report(totalBytesRead);
        }
    }
    
    private static void ThrowForNullResponse(HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new InvalidOperationException(@"Handler did not return a response message.");
        }
    }
    
    private static void ValidateArguments(Stream source, Stream destination, int bufferSize)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);

        if (!source.CanRead)
        {
            throw new ArgumentException(@"Has to be readable", nameof(source));
        }

        if (!destination.CanWrite)
        {
            throw new ArgumentException(@"Has to be writable", nameof(destination));
        }
        
        if (bufferSize < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bufferSize));
        }
    }
}

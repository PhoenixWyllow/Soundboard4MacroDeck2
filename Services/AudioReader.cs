using NAudio.Extras;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.IO;

namespace Soundboard4MacroDeck.Services;

/// <summary>
/// AudioReader simplifies opening an audio file
/// Simply pass in the bytes, and it will attempt to read the file 
/// It provides a volume property and implements both WaveStream and ISampleProvider, 
/// making it possibly the only stage in the audio pipeline necessary for simple playback scenarios
/// </summary>
public class AudioReader : WaveStream, ISampleProvider, IDisposable
{
    private Stream sourceStream; // take the byte array and hold it here
    private WaveStream readerStream; // the waveStream which we will use for all positioning
    private readonly IWaveProvider waveProvider;
    private bool isDisposed;
    private readonly SampleChannel _sampleChannel; // sample provider that gives us most stuff we need

    /// <summary>
    /// Initializes a new instance of AudioFileReader
    /// </summary>
    /// <param name="fileName">The file to open</param>
    /// <param name="fileData"></param>
    public AudioReader(string fileName, byte[] fileData, bool loopingEnabled)
    {
        FileName = fileName;
        sourceStream = new MemoryStream(fileData);
        readerStream = CreateReaderStream(sourceStream, fileName, loopingEnabled);
        _sampleChannel = new(readerStream, false);
        waveProvider = _sampleChannel.ToWaveProvider();
    }

    public IWaveProvider WaveProvider => waveProvider;

    /// <summary>
    /// File Name
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// Gets or Sets the Volume of this AudioFileReader. 1.0f is full volume
    /// </summary>
    public float Volume
    {
        get => _sampleChannel.Volume;
        set => _sampleChannel.Volume = value;
    }

    /// <inheritdoc />
    public int Read(float[] buffer, int offset, int count)
    {
        return _sampleChannel.Read(buffer, offset, count);
    }

    /// <summary>
    /// Position of this stream (in bytes)
    /// </summary>
    public override long Position
    {
        get => readerStream.Position;
        set => readerStream.Position = value;
    }

    public override TimeSpan CurrentTime 
    {
        get => readerStream.CurrentTime;
        set => readerStream.CurrentTime = value;
    }

    /// <summary>
    /// Length of this stream (in bytes)
    /// </summary>
    public override long Length => readerStream.Length;

    /// <summary>
    /// WaveFormat of this stream
    /// </summary>
    public override WaveFormat WaveFormat => readerStream.WaveFormat;

    public override TimeSpan TotalTime => readerStream.TotalTime;

    public override int Read(byte[] buffer, int offset, int count)
    {
        return waveProvider.Read(buffer, offset, count);
    }

    /// <summary>
    /// Creates the reader stream, supporting all filetypes in the core NAudio library,
    /// and ensuring we are in PCM format
    /// </summary>
    /// <param name="fileName">File Name</param>
    private static WaveStream CreateReaderStream(Stream source, string fileName, bool loopingEnabled)
    {
        WaveStream readerStream;
        if (fileName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
        {
            readerStream = new WaveFileReader(source);
            if (readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm && readerStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
            {
                readerStream = WaveFormatConversionStream.CreatePcmStream(readerStream);
                readerStream = new BlockAlignReductionStream(readerStream);
            }
        }
        //MediaFoundationReader is default reader for mp3
        //else if (fileName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
        //{
        //    readerStream = new Mp3FileReader(source);
        //}
        else if (fileName.EndsWith(".aiff", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".aif", StringComparison.OrdinalIgnoreCase))
        {
            readerStream = new AiffFileReader(source);
        }
        else
        {
            // fall back to media foundation reader, see if that can play it
            readerStream = new StreamMediaFoundationReader(source);
        }

        if (loopingEnabled)
        {
            readerStream = new LoopStream(readerStream);
        }
        return readerStream;
    }


    /// <summary>
    /// Disposes this AudioFileReader
    /// </summary>
    /// <param name="disposing">True if called from Dispose</param>
    protected override void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                if (readerStream is not null)
                {
                    readerStream.Dispose();
                    readerStream = null;
                }
                if (sourceStream is not null)
                {
                    sourceStream.Dispose();
                    sourceStream = null;
                }
            }

            isDisposed = true;
        }
    }

    public new void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
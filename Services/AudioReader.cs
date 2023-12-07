using NAudio.Extras;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Soundboard4MacroDeck.Services;

/// <summary>
/// AudioFileReader simplifies opening an audio file in NAudio
/// Simply pass in the bytes, and it will attempt to open the
/// file and set up a conversion path that turns into PCM IEEE float.
/// ACM codecs will be used for conversion.
/// It provides a volume property and implements both WaveStream and
/// ISampleProvider, making it possibly the only stage in your audio
/// pipeline necessary for simple playback scenarios
/// </summary>
public class AudioReader : WaveStream, ISampleProvider
{
    private Stream _sourceStream; // take the byte array and hold it here
    private WaveStream _readerStream; // the waveStream which we will use for all positioning
    private readonly SampleChannel _sampleChannel; // sample provider that gives us most stuff we need
    private readonly ISampleProvider sampleProvider;

    /// <summary>
    /// Initializes a new instance of AudioFileReader
    /// </summary>
    /// <param name="fileName">The file to open</param>
    /// <param name="fileData"></param>
    public AudioReader(string fileName, byte[] fileData, bool loopingEnabled)
    {
        FileName = fileName;
        _sourceStream = new MemoryStream(fileData);
        CreateReaderStream(fileName, loopingEnabled);
        _sampleChannel = new(_readerStream, false);
        WaveFormat = _readerStream.WaveFormat;
        sampleProvider = _readerStream.ToSampleProvider();
    }

    /// <summary>
    /// Creates the reader stream, supporting all filetypes in the core NAudio library,
    /// and ensuring we are in PCM format
    /// </summary>
    /// <param name="fileName">File Name</param>
    private void CreateReaderStream(string fileName, bool loopingEnabled)
    {
        if (fileName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
        {
            _readerStream = new WaveFileReader(_sourceStream);
            if (_readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm && _readerStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
            {
                _readerStream = WaveFormatConversionStream.CreatePcmStream(_readerStream);
                _readerStream = new BlockAlignReductionStream(_readerStream);
            }
        }
        else if (fileName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
        {
            _readerStream = new Mp3FileReader(_sourceStream);
        }
        else if (fileName.EndsWith(".aiff", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".aif", StringComparison.OrdinalIgnoreCase))
        {
            _readerStream = new AiffFileReader(_sourceStream);
        }
        else
        {
            // fall back to media foundation reader, see if that can play it
            _readerStream = new StreamMediaFoundationReader(_sourceStream);
        }

        if (loopingEnabled)
        {
            _readerStream = new LoopStream(_readerStream);
        }
    }
    /// <summary>
    /// File Name
    /// </summary>
    public string FileName { get; }

    /// <inheritdoc />
    public int Read(float[] buffer, int offset, int count)
    {
        return sampleProvider.Read(buffer, offset, count);
    }

    /// <summary>
    /// WaveFormat of this stream
    /// </summary>
    public override WaveFormat WaveFormat { get; }

    /// <summary>
    /// Length of this stream (in bytes)
    /// </summary>
    public override long Length => _sourceStream.Length;

    /// <summary>
    /// Position of this stream (in bytes)
    /// </summary>
    public override long Position
    {
        get => _readerStream.Position;
        set => _readerStream.Position = value;
    }

    public override TimeSpan CurrentTime { get => _readerStream.CurrentTime; set => _readerStream.CurrentTime = value; }
    public override TimeSpan TotalTime => _readerStream.TotalTime;

    /// <summary>
    /// Reads from this wave stream, choosing whether to loop or read once
    /// </summary>
    /// <param name="buffer">Audio buffer</param>
    /// <param name="offset">Offset into buffer</param>
    /// <param name="count">Number of bytes required</param>
    /// <returns>Number of bytes read</returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
        return _readerStream.Read(buffer, offset, count);
    }

    /// <summary>
    /// Gets or Sets the Volume of this AudioFileReader. 1.0f is full volume
    /// </summary>
    public float Volume
    {
        get => _sampleChannel.Volume;
        set => _sampleChannel.Volume = value;
    }

    /// <summary>
    /// Disposes this AudioFileReader
    /// </summary>
    /// <param name="disposing">True if called from Dispose</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_readerStream != null)
            {
                _readerStream.Dispose();
                _readerStream = null;
            }
            if (_sourceStream != null)
            {
                _sourceStream.Dispose();
                _sourceStream = null;
            }
        }
        base.Dispose(disposing);
    }
}
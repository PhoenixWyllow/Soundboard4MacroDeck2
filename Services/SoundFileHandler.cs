using System;
using System.IO;
using NAudio.Wave;

namespace MacroDeckSoundboard.Services
{
    public sealed class SoundFileHandler : IDisposable
    {
        private bool disposedValue;
        private MemoryStream InStream { get; set; }
        private BinaryReader Reader { get; set; }
        public RawSourceWaveStream RawSource { get; set; }

        public SoundFileHandler(byte[] fileData)
        {
            InStream = new MemoryStream(fileData);
            Reader = new BinaryReader(InStream);
            RawSource = new RawSourceWaveStream(InStream, new WaveFormat(Reader));
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    InStream.Dispose();
                    Reader.Dispose();
                    RawSource.Dispose();
                }

                InStream = null;
                Reader = null;
                RawSource = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

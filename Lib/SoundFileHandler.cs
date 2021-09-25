using System;
using System.IO;
using NAudio.Wave;

namespace MacroDeckSoundboard.Lib
{
    public sealed class SoundFileHandler : IDisposable
    {
        private bool disposedValue;
        private MemoryStream InStream { get; set; }
        private BinaryReader Reader { get; set; }
        public RawSourceWaveStream RawSource { get; set; }

        internal static SoundFileHandler Init(byte[] fileData)
        {
            SoundFileHandler sfh = new SoundFileHandler();
            sfh.InStream = new MemoryStream(fileData);
            sfh.Reader = new BinaryReader(sfh.InStream);
            sfh.RawSource = new RawSourceWaveStream(sfh.InStream, new WaveFormat(sfh.Reader));
            return sfh;
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

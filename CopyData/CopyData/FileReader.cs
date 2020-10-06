using System;
using System.IO;
using System.Text;

namespace CopyData
{
    public class FileReader: IDataSource, IDisposable
    {
        private readonly BinaryReader _stream;
        private readonly int _bufferSize; 

        public FileReader(string fileName, int bufferSize, Encoding encoding)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName))
                throw new ArgumentException(nameof(fileName));
            this._bufferSize = bufferSize;
            _stream = new BinaryReader (File.Open(fileName, FileMode.Open), encoding ?? Encoding.Default);
        }

        public int GetData(out char[] buf)
        {
            buf = _stream.ReadChars(_bufferSize);
            return buf.Length;
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}
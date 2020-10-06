using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CopyData
{
    public class FileWriter : BaseDataDestination
    {
        private readonly StreamWriter _stream;

        public FileWriter(string fileName, Encoding encoding)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            _stream = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate), encoding ?? Encoding.Default);
        }

        protected override async Task PutChunkDataAsync(char[] buf, int count)
        {
            if (count > 0)
                await _stream.WriteAsync(buf);
            else
                _stream.Close();
        }

        public override void Dispose()
        {
            _stream?.Dispose();
        }
    }
}
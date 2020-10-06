using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CopyData
{
    public class FileWriter : IDataDestination, IDisposable
    {
        private readonly StreamWriter stream;

        public FileWriter(string fileName, Encoding encoding)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            stream = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate), encoding ?? Encoding.Default);
        }

        public async Task PutData(char[] buf, int count)
        {
            if (count > 0)
                await stream.WriteAsync(buf);
            else
                stream.Close();
        }

        public void Dispose()
        {
            stream?.Dispose();
        }
    }
}
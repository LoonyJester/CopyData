using System;
using System.Threading.Tasks;

namespace CopyData
{
    internal class Copier : IDisposable
    {
        private readonly IDataSource _source;
        private readonly IDataDestination _destination;

        public Copier(IDataSource source, IDataDestination destination)
        {
            _source = source;
            _destination = destination;
        }

        public async Task CopyAsync()
        {
            int count = -1;
            char[] buffer;
            while (count != 0)
            {
                count = _source.GetData(out buffer);
                await _destination.PutData(buffer, count);
            }
        }

        public void Dispose()
        {
            _source?.Dispose();
            _destination?.Dispose();
        }
    }
}

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
            this._source = source;
            this._destination = destination;
        }

        public async Task Copy()
        {
            int count = -1;
            char[] buffer = null;
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

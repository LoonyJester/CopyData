using System.Threading.Tasks;

namespace CopyData
{
    public abstract class BaseDataDestination: IDataDestination
    {
        private char[] _buffer;
        private int _pointer;

        public int StrictBufferSizeAsync { get; } = 512;
        public async Task PutData(char[] buf, int count)
        {
            if (StrictBufferSizeAsync == 0)
            {
                await PutChunkDataAsync(buf, count);
                return;
            }

            if (_pointer == 0 && count == StrictBufferSizeAsync)
            {
                await PutChunkDataAsync(buf, StrictBufferSizeAsync);
                return;
            }

            if (count == 0)
            {
                if (_pointer > 0)
                    await PutChunkDataAsync(_buffer, _pointer);
                await PutChunkDataAsync(buf, 0);
                return;
            }

            int processedCount = 0;
            while (processedCount < count - 1)
            {
                if(_pointer == 0)
                    _buffer = new char[StrictBufferSizeAsync];
                for (; _pointer < StrictBufferSizeAsync && processedCount < count; _pointer++, processedCount++)
                {
                    _buffer[_pointer] = buf[processedCount];
                }

                if (_pointer == StrictBufferSizeAsync)
                {
                    await PutChunkDataAsync(buf, _pointer);
                    _pointer = 0;
                }
            }
        }

        protected abstract Task PutChunkDataAsync(char[] buf, int count);

        public abstract void Dispose();
    }
}
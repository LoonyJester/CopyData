using System;

namespace CopyData
{
    internal interface IDataSource: IDisposable
    {
        int GetData(out char[] buf);
    }
}
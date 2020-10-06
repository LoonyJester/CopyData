using System;
using System.Threading.Tasks;

namespace CopyData
{
    internal interface IDataDestination: IDisposable
    {
        Task PutData(char[] buf, int count);
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSC.Core.Base.Infrastructure.Interface
{
    public interface IAsyncTransactionRepository
    {
        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();

        ValueTask DisposeTransactionAsync();
    }
}

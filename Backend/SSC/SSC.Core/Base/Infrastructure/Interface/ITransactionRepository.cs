using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Infrastructure.Interface
{
    public interface ITransactionRepository
    {
        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        void DisposeTransaction();
    }
}

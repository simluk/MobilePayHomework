using System;
using System.Collections.Generic;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface ITransactionSource
    {
        IEnumerable<ITransaction> FetchTransactions(ITransactionInputFormat format, ITransactionContext context);
    }
}

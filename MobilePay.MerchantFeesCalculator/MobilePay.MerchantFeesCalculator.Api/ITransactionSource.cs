using System;
using System.Collections.Generic;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <summary>
    /// Generic <seealso cref="ITransaction"/> source reader
    /// </summary>
    public interface ITransactionSource
    {
        IEnumerable<ITransaction> FetchTransactions(ITransactionInputFormat format, ITransactionContext context);
    }
}

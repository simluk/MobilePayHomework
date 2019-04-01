using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface ITransactionContext
    {
        IReadOnlyCollection<IMerchant> Merchants { get; }

        // use plain List to ensure order
        IReadOnlyDictionary<IMerchant, List<ITransaction>> Transactions { get; }

        void ApplyDiscount(string merchantName, ITransactionDiscount discount);

        void LogTransaction(ITransaction transaction);
    }
}

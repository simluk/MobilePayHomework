using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <summary>
    /// Root object for <see cref="ITransactionFeeCalculator"/> context data
    /// </summary>
    public interface ITransactionContext
    {
        /// <summary>
        /// Merchants available in the system
        /// </summary>
        IReadOnlyCollection<IMerchant> Merchants { get; }

        /// <summary>
        /// Cached transactions
        /// </summary>
        /// <remarks>
        /// Use plain List to ensure order
        /// </remarks>
        IReadOnlyDictionary<IMerchant, List<ITransaction>> Transactions { get; }

        /// <summary>
        /// Apply discount to the <paramref name="merchantName"/> <seealso cref="IMerchant"/>
        /// </summary>
        void ApplyDiscount(string merchantName, ITransactionDiscount discount);

        /// <summary>
        /// Log <seealso cref="ITransaction"/> to the system
        /// </summary>
        void LogTransaction(ITransaction transaction);
    }
}

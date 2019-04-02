using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <inheritdoc />
    /// <summary>
    /// Generic transaction discount interface
    /// </summary>
    public interface ITransactionDiscount : IEquatable<ITransactionDiscount>
    {
        decimal Compute(decimal transactionAmount);
        bool ContainsDiscount { get; }
    }
}

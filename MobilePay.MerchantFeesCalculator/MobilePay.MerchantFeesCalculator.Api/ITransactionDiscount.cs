using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface ITransactionDiscount : IEquatable<ITransactionDiscount>
    {
        decimal Compute(decimal transactionAmount);
        bool ContainsDiscount { get; }
    }
}

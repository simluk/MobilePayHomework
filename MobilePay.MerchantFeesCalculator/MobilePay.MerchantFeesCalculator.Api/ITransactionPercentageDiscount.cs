using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <summary>
    /// Percentage based <seealso cref="ITransactionDiscount"/> 
    /// </summary>
    public interface ITransactionPercentageDiscount : ITransactionDiscount
    {
        decimal Percentage { get; }
    }
}

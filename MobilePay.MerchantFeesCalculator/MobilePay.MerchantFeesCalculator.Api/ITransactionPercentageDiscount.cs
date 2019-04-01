using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface ITransactionPercentageDiscount : ITransactionDiscount
    {
        decimal Percentage { get; }
    }
}

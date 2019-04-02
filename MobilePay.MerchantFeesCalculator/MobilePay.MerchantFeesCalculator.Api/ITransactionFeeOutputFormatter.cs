using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <summary>
    /// Interface used to format <seealso cref="ITransactionFee"/> for output
    /// </summary>
    public interface ITransactionFeeOutputFormatter
    {
        string Format(ITransactionFee fee);
        void Format(ITransactionFee fee, StringBuilder builder);
    }
}

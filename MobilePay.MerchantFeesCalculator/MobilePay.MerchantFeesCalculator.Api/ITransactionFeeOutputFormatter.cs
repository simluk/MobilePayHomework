using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface ITransactionFeeOutputFormatter
    {
        string Format(ITransactionFee fee);
        void Format(ITransactionFee fee, StringBuilder builder);
    }
}

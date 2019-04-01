using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface ITransactionFeeOutputWriter
    {
        void Write(ITransactionFee fee, ITransactionFeeOutputFormatter formatter);
    }
}

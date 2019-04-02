using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <summary>
    /// Interface to write <seealso cref="ITransactionFee"/> to output
    /// </summary>
    public interface ITransactionFeeOutputWriter
    {
        void Write(ITransactionFee fee, ITransactionFeeOutputFormatter formatter);
    }
}

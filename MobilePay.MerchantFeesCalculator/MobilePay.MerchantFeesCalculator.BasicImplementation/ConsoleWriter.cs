using System;
using System.Collections.Generic;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class ConsoleWriter : ITransactionFeeOutputWriter
    {
        #region Implementation of ITransactionFeeOutputWriter

        /// <inheritdoc />
        public void Write(ITransactionFee fee, ITransactionFeeOutputFormatter formatter)
        {
            Console.WriteLine(formatter.Format(fee));
        }

        #endregion
    }
}

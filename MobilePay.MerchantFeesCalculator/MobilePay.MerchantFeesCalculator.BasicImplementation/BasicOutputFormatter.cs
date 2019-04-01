using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class BasicOutputFormatter : ITransactionFeeOutputFormatter
    {
        #region Implementation of ITransactionFeeOutputFormatter

        /// <inheritdoc />
        public string Format(ITransactionFee fee) => InternalFormat(fee);

        /// <inheritdoc />
        public void Format(ITransactionFee fee, StringBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            builder.Append(InternalFormat(fee));
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string InternalFormat(ITransactionFee fee)
        {
            if (fee == null) throw new ArgumentNullException(nameof(fee));

            // TODO: optimize performance if needed
            return $"{fee.Transaction.Date:yyyy-MM-dd} {fee.Transaction.Merchant.Name.PadRight(10)} {fee.Value:C}";
        }
    }
}

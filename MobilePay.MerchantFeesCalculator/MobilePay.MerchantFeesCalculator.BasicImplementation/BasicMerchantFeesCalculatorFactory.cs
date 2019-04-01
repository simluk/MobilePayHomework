using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class BasicMerchantFeesCalculatorFactory : IMerchantFeesCalculatorFactory
    {
        #region Implementation of IMerchantFeesCalculatorFactory

        /// <inheritdoc />
        public ITransactionSource CreateTextBasedTransactionSource(string filePath) => new TextFileTransactionSource(filePath);

        /// <inheritdoc />
        public ITransactionInputFormat CreateTransactionInputFormat() => new BasicTransactionInputFormat();

        /// <inheritdoc />
        public ITransactionFeeCalculator CreateTransactionFeeCalculator(decimal monthlyFee) => new BasicTransactionFeeCalculator(monthlyFee);

        /// <inheritdoc />
        public ITransactionFeeOutputWriter CreateOutputWriter() => new ConsoleWriter();

        /// <inheritdoc />
        public ITransactionFeeOutputFormatter CreateOutputFormatter() => new BasicOutputFormatter();

        /// <inheritdoc />
        public ITransactionPercentageDiscount CreatePercentageDiscount(decimal percentage) => new TransactionPercentageDiscount(percentage);

        #endregion
    }
}

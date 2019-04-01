using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface IMerchantFeesCalculatorFactory
    {
        ITransactionSource CreateTextBasedTransactionSource(string filePath);
        ITransactionInputFormat CreateTransactionInputFormat();
        ITransactionFeeCalculator CreateTransactionFeeCalculator(decimal monthlyFee);
        ITransactionFeeOutputWriter CreateOutputWriter();
        ITransactionFeeOutputFormatter CreateOutputFormatter();

        ITransactionPercentageDiscount CreatePercentageDiscount(decimal percentage);
    }
}

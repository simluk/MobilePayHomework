using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface ITransactionFeeCalculator
    {
        ITransactionFee Compute(ITransaction transaction, ITransactionContext context);
    }
}

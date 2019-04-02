using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <summary>
    /// Root interface for computation of <seealso cref="ITransaction"/> <seealso cref="ITransactionFee"/>
    /// </summary>
    public interface ITransactionFeeCalculator
    {
        ITransactionFee Compute(ITransaction transaction, ITransactionContext context);
    }
}

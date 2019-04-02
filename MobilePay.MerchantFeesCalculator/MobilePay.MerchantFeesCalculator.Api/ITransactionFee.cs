using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <summary>
    /// Interface representing the <seealso cref="decimal"/> fee for the <seealso cref="ITransactionFee.Transaction"/>
    /// </summary>
    public interface ITransactionFee
    {
        decimal Value { get; }
        ITransaction Transaction { get; }
    }
}

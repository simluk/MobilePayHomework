using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface ITransactionFee
    {
        decimal Value { get; }
        ITransaction Transaction { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface ITransaction
    {
        DateTime Date { get; }
        IMerchant Merchant { get; }
        decimal Amount { get; }
    }
}

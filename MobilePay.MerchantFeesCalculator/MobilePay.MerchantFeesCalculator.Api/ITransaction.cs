using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <summary>
    /// This interface contains all relevant information about transaction 
    /// </summary>
    public interface ITransaction
    {
        DateTime Date { get; }
        IMerchant Merchant { get; }
        decimal Amount { get; }
    }
}

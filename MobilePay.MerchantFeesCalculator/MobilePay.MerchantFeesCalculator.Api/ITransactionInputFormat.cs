using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface ITransactionInputFormat
    {
        ITransaction Parse(string line, ITransactionContext context);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <summary>
    /// Interface to parse input and generate <seealso cref="ITransaction"/>
    /// </summary>
    public interface ITransactionInputFormat
    {
        ITransaction Parse(string line, ITransactionContext context);
    }
}

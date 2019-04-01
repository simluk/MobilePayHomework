using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface IFileBasedTransactionSource : ITransactionSource
    {
        string FilePath { get; }
    }
}

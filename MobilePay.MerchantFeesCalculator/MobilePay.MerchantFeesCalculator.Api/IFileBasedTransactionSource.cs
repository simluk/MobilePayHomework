using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <summary>
    /// Specific type of <seealso cref="ITransactionSource"/> where <seealso cref="ITransaction"/>'s are read from <seealso cref="IFileBasedTransactionSource.FilePath"/>
    /// </summary>
    public interface IFileBasedTransactionSource : ITransactionSource
    {
        string FilePath { get; }
    }
}

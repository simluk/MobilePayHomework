using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    /// <inheritdoc />
    /// <summary>
    /// This interface contains all relevant information about merchant
    /// </summary>
    public interface IMerchant : IEquatable<IMerchant>
    {
        string Name { get; }
        ITransactionDiscount Discount { get; }
    }
}

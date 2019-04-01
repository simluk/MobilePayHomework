using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePay.MerchantFeesCalculator.Api
{
    public interface IMerchant : IEquatable<IMerchant>
    {
        string Name { get; }
        ITransactionDiscount Discount { get; }
    }
}

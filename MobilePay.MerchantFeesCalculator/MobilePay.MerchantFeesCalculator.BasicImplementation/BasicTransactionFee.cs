using System;
using System.Collections.Generic;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class BasicTransactionFee : ITransactionFee
    {
        #region Implementation of ITransactionFee

        /// <inheritdoc />
        public decimal Value { get; }

        /// <inheritdoc />
        public ITransaction Transaction { get; }

        #endregion

        #region ctor

        /// <inheritdoc />
        public BasicTransactionFee(decimal value, ITransaction transaction) => (Value, Transaction) = (value, transaction);

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(Value)}: {Value}, {nameof(Transaction)}: {Transaction}";
        }
    }
}

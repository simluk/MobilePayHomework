using System;
using System.Collections.Generic;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class BasicTransaction : ITransaction
    {
        #region Implementation of ITransaction

        /// <inheritdoc />
        public DateTime Date { get; }

        /// <inheritdoc />
        public IMerchant Merchant { get; }

        /// <inheritdoc />
        public decimal Amount { get; }

        #endregion

        #region ctor

        public BasicTransaction(DateTime date, IMerchant merchant, decimal amount) =>
            (Date, Merchant, Amount) = (date, merchant, amount);

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(Date)}: {Date}, {nameof(Merchant)}: {Merchant}, {nameof(Amount)}: {Amount}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class TransactionPercentageDiscount : ITransactionPercentageDiscount, IEquatable<TransactionPercentageDiscount>
    {
        #region Implementation of ITransactionDiscount

        /// <inheritdoc />
        public decimal Compute(decimal transactionAmount)
        {
            if (Percentage == 0) return 0;
            return transactionAmount * (Percentage / 100m);
        }

        /// <inheritdoc />
        public bool ContainsDiscount => Percentage > 0;

        #endregion

        #region Implementation of ITransactionPercentageDiscount

        /// <inheritdoc />
        public decimal Percentage { get; }

        #endregion

        /// <inheritdoc />
        public TransactionPercentageDiscount(decimal percentage) => Percentage = percentage;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(Percentage)}: {Percentage}";
        }

        #region Equality members

        /// <inheritdoc />
        public bool Equals(TransactionPercentageDiscount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Percentage == other.Percentage;
        }

        /// <inheritdoc />
        public bool Equals(ITransactionDiscount other)
        {
            return Equals((object) other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TransactionPercentageDiscount) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Percentage.GetHashCode();
        }

        public static bool operator ==(TransactionPercentageDiscount left, TransactionPercentageDiscount right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TransactionPercentageDiscount left, TransactionPercentageDiscount right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}

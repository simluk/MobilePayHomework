using System;
using System.Collections.Generic;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class TransactionNoDiscount : ITransactionDiscount, IEquatable<TransactionNoDiscount>
    {
        #region Implementation of ITransactionDiscount

        /// <inheritdoc />
        public decimal Compute(decimal transactionAmount) => 0;

        /// <inheritdoc />
        public bool ContainsDiscount => false;

        #endregion

        #region Equality members

        /// <inheritdoc />
        public bool Equals(TransactionNoDiscount other)
        {
            return ContainsDiscount.Equals(other.ContainsDiscount);
        }

        /// <inheritdoc />
        public bool Equals(ITransactionDiscount other)
        {
            return Equals((object)other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TransactionNoDiscount) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 23 + ContainsDiscount.GetHashCode();
            return hash;
        }

        public static bool operator ==(TransactionNoDiscount left, TransactionNoDiscount right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TransactionNoDiscount left, TransactionNoDiscount right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}

using System;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class BasicMerchant : IMerchant, IEquatable<BasicMerchant>
    {
        #region Implementation of IMerchant

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public ITransactionDiscount Discount { get; internal set; }

        #endregion

        #region ctor

        public BasicMerchant(string name, ITransactionDiscount discount) => (Name, Discount) = (name, discount);

        #endregion

        #region Equality members

        /// <inheritdoc />
        public bool Equals(BasicMerchant other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Name.Equals(other.Name) &&
                   Discount.Equals(other.Discount);
        }

        /// <inheritdoc />
        public bool Equals(IMerchant other)
        {
            return Equals((object)other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BasicMerchant)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 23 + Name.GetHashCode();
            hash = hash * 23 + Discount.GetHashCode();
            return hash;
        }

        public static bool operator ==(BasicMerchant left, BasicMerchant right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BasicMerchant left, BasicMerchant right)
        {
            return !Equals(left, right);
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}

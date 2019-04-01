using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class SimpleTransactionContext : ITransactionContext
    {
        private static readonly IMerchant[] _merchants;
        private readonly Dictionary<IMerchant, List<ITransaction>> _transactions;

        static SimpleTransactionContext()
        {
            // for sake of testing load merchants here
            _merchants = new IMerchant[]
            {
                new BasicMerchant("7-ELEVEN", new TransactionNoDiscount()),
                new BasicMerchant("CIRCLE_K", new TransactionNoDiscount()),
                new BasicMerchant("TELIA", new TransactionNoDiscount()),
                new BasicMerchant("NETTO", new TransactionNoDiscount())
            };
        }

        public SimpleTransactionContext() => _transactions = new Dictionary<IMerchant, List<ITransaction>>();

        #region Implementation of ITransactionContext

        /// <inheritdoc />
        public IReadOnlyCollection<IMerchant> Merchants => _merchants;

        /// <inheritdoc />
        public IReadOnlyDictionary<IMerchant, List<ITransaction>> Transactions => _transactions;

        /// <inheritdoc />
        public void ApplyDiscount(string merchantName, ITransactionDiscount discount)
        {
            var basicMerchant = (BasicMerchant)_merchants.FirstOrDefault(merchant => string.Equals(merchant.Name, merchantName));
            if (basicMerchant == null) throw new ArgumentException($"'{merchantName}' was not found");

            basicMerchant.Discount = discount;
        }

        /// <inheritdoc />
        public void LogTransaction(ITransaction transaction)
        {
            if (!_transactions.ContainsKey(transaction.Merchant))
            {
                _transactions.Add(transaction.Merchant, new List<ITransaction>());
            }

            _transactions[transaction.Merchant].Add(transaction);
        }

        #endregion
    }
}

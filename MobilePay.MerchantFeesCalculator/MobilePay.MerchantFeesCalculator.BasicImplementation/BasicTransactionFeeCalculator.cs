using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class BasicTransactionFeeCalculator : ITransactionFeeCalculator
    {
        private decimal MonthlyFee { get; }

        public BasicTransactionFeeCalculator(decimal monthlyFee) => MonthlyFee = monthlyFee;


        #region Implementation of ITransactionFeeCalculator

        /// <inheritdoc />
        public ITransactionFee Compute(ITransaction transaction, ITransactionContext context)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            // TODO: below should probable be wrapped inside computation items in order to be more flexible when adding new stuff

            // regular 1% fee
            var fee = transaction.Amount * 0.01m;

            // add discount if applicable
            if (transaction.Merchant.Discount.ContainsDiscount)
            {
                fee = fee - transaction.Merchant.Discount.Compute(fee);
            }

            // first transaction in month - apply fixed fee
            var merchantTransactions = context.Transactions[transaction.Merchant];

            // assuming this transaction has been already added to the context - perform assert to be safe
            Debug.Assert(merchantTransactions.Last() == transaction);

            // get previous transaction - remember - transactions are ordered based on time
            var previousTransaction =
                merchantTransactions.Count > 1 ? merchantTransactions[merchantTransactions.Count - 2] : null;

            // no previous transaction or it was in previous month
            if (previousTransaction == null ||
                previousTransaction.Date.Month < transaction.Date.Month)
            {
                fee += MonthlyFee;
            }

            return new BasicTransactionFee(fee, transaction);
        }

        #endregion
    }
}

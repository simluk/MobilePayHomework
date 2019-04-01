using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class BasicTransactionInputFormat : ITransactionInputFormat
    {
        private const char Separator = ' ';

        #region Implementation of ITransactionInputFormat

        /// <inheritdoc />
        public ITransaction Parse(string line, ITransactionContext context)
        {
            if (line == null) throw new ArgumentNullException(nameof(line));
            if (context == null) throw new ArgumentNullException(nameof(context));

            // Date merchantName amount
            var splitLine = line
                .Trim()             // get rid of the side whitespace
                .Split(Separator)   // get the parts for parsing
                .Where(part => !string.IsNullOrWhiteSpace(part))    // get rid of the empty parts between real data
                .ToArray();         // enumerate to avoid multiple enumeration

            if (splitLine.Length != 3) throw new ConstraintException($"Input {nameof(line)} is not in correct format. {nameof(BasicTransactionInputFormat)} expects 'Date merchantName amount' format.");

            // let these throw if incorrect format
            var date = DateTime.Parse(splitLine[0]);
            var amount = decimal.Parse(splitLine[2]);

            var mappedMerchant = context.Merchants.FirstOrDefault(merchant => string.Equals(merchant.Name, splitLine[1]));
            if (mappedMerchant == null) throw new ArgumentOutOfRangeException($"Could not locate parsed merchant in {nameof(context)} database.");

            return new BasicTransaction(date, mappedMerchant, amount);
        }

        #endregion
    }
}

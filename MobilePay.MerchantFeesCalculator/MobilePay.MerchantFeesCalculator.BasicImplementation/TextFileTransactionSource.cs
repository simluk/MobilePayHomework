using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.BasicImplementation
{
    internal class TextFileTransactionSource : IFileBasedTransactionSource
    {

        #region Implementation of IFileBasedTransactionSource

        /// <inheritdoc />
        public string FilePath { get; }

        #endregion

        #region Implementation of ITransactionSource

        /// <inheritdoc />
        public IEnumerable<ITransaction> FetchTransactions(ITransactionInputFormat format, ITransactionContext context)
        {
            if (format == null) throw new ArgumentNullException(nameof(format));
            if (!File.Exists(FilePath)) throw new InvalidOperationException($"Specified '{FilePath}' could not be found to read transactions from.");

            using (var reader = new StreamReader(FilePath))
            {
                while (reader.Peek() >= 0)
                {
                    yield return format.Parse(reader.ReadLine(), context);
                }
            }
        }

        #endregion

        #region ctor

        /// <inheritdoc />
        public TextFileTransactionSource(string filePath) => FilePath = filePath;

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(FilePath)}: {FilePath}";
        }
    }
}

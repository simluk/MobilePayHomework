using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using MobilePay.MerchantFeesCalculator.Api;
using NUnit.Framework;

namespace MobilePay.MerchantFeesCalculator.IntegrationTests
{
    [TestFixture]
    internal class FullCalculationFromFileTest
    {
        private class TestOutputWriter : ITransactionFeeOutputWriter, IDisposable
        {
            private StreamReader _assertFile;

            /// <inheritdoc />
            public TestOutputWriter(string assertFile)
            {
                _assertFile = new StreamReader(assertFile);
            }

            #region Implementation of ITransactionFeeOutputWriter

            /// <inheritdoc />
            public void Write(ITransactionFee fee, ITransactionFeeOutputFormatter formatter)
            {
                var line = formatter.Format(fee);

                if (_assertFile.Peek() >= 0)
                {
                    var assertLine = _assertFile.ReadLine();
                    Assert.AreEqual(line, assertLine, "Transaction fee formatted lines are different.");
                }
                else
                {
                    Assert.Fail("Too many transactions written!");
                }
            }

            #endregion

            #region IDisposable

            /// <inheritdoc />
            public void Dispose()
            {
                if (_assertFile.Peek() >= 0)
                {
                    Assert.Fail("Too few transactions written!");
                }

                _assertFile?.Dispose();
                _assertFile = null;
            }

            #endregion
        }

        [TestCase(@"Data\transactions.txt", @"Data\results.txt", "MobilePay.MerchantFeesCalculator.BasicImplementation.dll")]
        public void TestTransactionFeesFromFile(string transactionFilePath, string assertResultsFilePath, string implementerAssemblyName)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var factory = DynamicLoader.LoadModuleType<IMerchantFeesCalculatorFactory>(Path.Combine(assemblyPath, implementerAssemblyName));
            var context = DynamicLoader.LoadModuleType<ITransactionContext>(Path.Combine(assemblyPath, implementerAssemblyName));

            var source = factory.CreateTextBasedTransactionSource(transactionFilePath);
            var sourceFormat = factory.CreateTransactionInputFormat();
            var feeCalculator = factory.CreateTransactionFeeCalculator();
            var outputFormatter = factory.CreateOutputFormatter();

            context.ApplyDiscount("TELIA", factory.CreatePercentageDiscount(10));
            context.ApplyDiscount("CIRCLE_K", factory.CreatePercentageDiscount(20));

            using (var outputWriter = new TestOutputWriter(assertResultsFilePath)) // use custom test writer
            {
                foreach (var transaction in source.FetchTransactions(sourceFormat, context))
                {
                    context.LogTransaction(transaction);

                    var fee = feeCalculator.Compute(transaction, context);
                    outputWriter.Write(fee, outputFormatter);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MobilePay.MerchantFeesCalculator.Api;
using NUnit.Framework;

namespace MobilePay.MerchantFeesCalculator.AcceptanceTests
{
    [TestFixture]
    public class SimpleAcceptanceTest
    {
        private class TestHelper : ITransactionSource, ITransactionFeeOutputWriter
        {
            private string[] _transactionFeeResults;
            private int _indexer = 0;

            #region Implementation of ITransactionSource

            /// <inheritdoc />
            public IEnumerable<ITransaction> FetchTransactions(ITransactionInputFormat format, ITransactionContext context)
            {
               var transactionMock1 = new Moq.Mock<ITransaction>();
               transactionMock1.SetupGet(m => m.Amount).Returns(120);
               transactionMock1.SetupGet(m => m.Date).Returns(DateTime.Parse("2018-09-02"));
               transactionMock1.SetupGet(m => m.Merchant).Returns(context.Merchants.First(merchant => string.Equals(merchant.Name, "CIRCLE_K")));

               var transactionMock2 = new Moq.Mock<ITransaction>();
               transactionMock2.SetupGet(m => m.Amount).Returns(200);
               transactionMock2.SetupGet(m => m.Date).Returns(DateTime.Parse("2018-09-04"));
               transactionMock2.SetupGet(m => m.Merchant).Returns(context.Merchants.First(merchant => string.Equals(merchant.Name, "TELIA")));

               var transactionMock3 = new Moq.Mock<ITransaction>();
               transactionMock3.SetupGet(m => m.Amount).Returns(300);
               transactionMock3.SetupGet(m => m.Date).Returns(DateTime.Parse("2018-10-22"));
               transactionMock3.SetupGet(m => m.Merchant).Returns(context.Merchants.First(merchant => string.Equals(merchant.Name, "CIRCLE_K")));

                var transactionMock4 = new Moq.Mock<ITransaction>();
               transactionMock4.SetupGet(m => m.Amount).Returns(150);
               transactionMock4.SetupGet(m => m.Date).Returns(DateTime.Parse("2018-10-29"));
               transactionMock4.SetupGet(m => m.Merchant).Returns(context.Merchants.First(merchant => string.Equals(merchant.Name, "CIRCLE_K")));


                _transactionFeeResults = new[]
               {
                   "2018-09-02 CIRCLE_K   $1.20",
                   "2018-09-04 TELIA      $2.00",
                   "2018-10-22 CIRCLE_K   $3.00",
                   "2018-10-29 CIRCLE_K   $1.50"
               };

               return new[]
               {
                   transactionMock1.Object,
                   transactionMock2.Object,
                   transactionMock3.Object,
                   transactionMock4.Object
               }; ;
            }

            #endregion

            #region Implementation of ITransactionFeeOutputWriter

            /// <inheritdoc />
            public void Write(ITransactionFee fee, ITransactionFeeOutputFormatter formatter)
            {
                Assert.AreEqual(_transactionFeeResults[_indexer++], formatter.Format(fee));
            }

            #endregion
        }

        [TestCase("MobilePay.MerchantFeesCalculator.BasicImplementation.dll")]
        public void TestCase1(string implementerAssemblyName)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var factory = DynamicLoader.LoadModuleType<IMerchantFeesCalculatorFactory>(Path.Combine(assemblyPath, implementerAssemblyName));
            var context = DynamicLoader.LoadModuleType<ITransactionContext>(Path.Combine(assemblyPath, implementerAssemblyName));

            var source = new TestHelper();
            var outputWriter = source;
            var sourceFormat = factory.CreateTransactionInputFormat();
            var feeCalculator = factory.CreateTransactionFeeCalculator(0);
            var outputFormatter = factory.CreateOutputFormatter();

            foreach (var transaction in source.FetchTransactions(sourceFormat, context))
            {
                context.LogTransaction(transaction);

                var fee = feeCalculator.Compute(transaction, context);
                outputWriter.Write(fee, outputFormatter);
            }
        }
    }
}

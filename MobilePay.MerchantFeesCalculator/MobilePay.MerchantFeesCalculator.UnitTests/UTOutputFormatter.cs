using System;
using MobilePay.MerchantFeesCalculator.Api;
using NUnit.Framework;

namespace MobilePay.MerchantFeesCalculator.UnitTests
{
    [TestFixture]
    internal class UTOutputFormatter
    {
        [Test]
        public void TestSimpleFormatter()
        {
            var factory = DynamicLoader.LoadModuleType<IMerchantFeesCalculatorFactory>(
                "MobilePay.MerchantFeesCalculator.BasicImplementation.dll");
            var formatter = factory.CreateOutputFormatter();

            var transactionMock = new Moq.Mock<ITransaction>();
            transactionMock.SetupGet(m => m.Amount).Returns(100m);

            var transactionFeeMock = new Moq.Mock<ITransactionFee>();
            transactionFeeMock.SetupGet(m => m.Transaction.Date).Returns(DateTime.Parse("2001-02-03"));
            transactionFeeMock.SetupGet(m => m.Transaction.Merchant.Name).Returns("NORFA");
            transactionFeeMock.SetupGet(m => m.Value).Returns(20);

            var line = formatter.Format(transactionFeeMock.Object);

            Assert.AreEqual("2001-02-03 NORFA      $20.00", line);
        }
    }
}

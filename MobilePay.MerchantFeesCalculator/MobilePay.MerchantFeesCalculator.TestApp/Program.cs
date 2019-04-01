using System;
using MobilePay.MerchantFeesCalculator.Api;

namespace MobilePay.MerchantFeesCalculator.TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Incorrect syntax. Provide file for transactions.");
                return;
            }

            var transactionFilePath = args[0];
            var factory = DynamicLoader.LoadModuleType<IMerchantFeesCalculatorFactory>(
                "MobilePay.MerchantFeesCalculator.BasicImplementation.dll");
            var context = DynamicLoader.LoadModuleType<ITransactionContext>(
                "MobilePay.MerchantFeesCalculator.BasicImplementation.dll");

            var source = factory.CreateTextBasedTransactionSource(transactionFilePath);
            var sourceFormat = factory.CreateTransactionInputFormat();
            var feeCalculator = factory.CreateTransactionFeeCalculator(29);
            var outputWriter = factory.CreateOutputWriter();
            var outputFormatter = factory.CreateOutputFormatter();

            context.ApplyDiscount("TELIA", factory.CreatePercentageDiscount(10));
            context.ApplyDiscount("CIRCLE_K", factory.CreatePercentageDiscount(20));

            foreach (var transaction in source.FetchTransactions(sourceFormat, context))
            {
                context.LogTransaction(transaction);

                var fee = feeCalculator.Compute(transaction, context);
                outputWriter.Write(fee, outputFormatter);
            }

            Console.ReadKey();
        }
    }
}

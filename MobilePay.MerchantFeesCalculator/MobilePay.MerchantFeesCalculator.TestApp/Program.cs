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

            // read input file name
            var transactionFilePath = args[0];

            // retrieve root factory and context objects from implementing assembly

            // note that "MobilePay.MerchantFeesCalculator.BasicImplementation.dll" is implemented all internal so that
            // client applications would use API to access the transaction fee computation objects
            var factory = DynamicLoader.LoadModuleType<IMerchantFeesCalculatorFactory>(
                "MobilePay.MerchantFeesCalculator.BasicImplementation.dll");
            var context = DynamicLoader.LoadModuleType<ITransactionContext>(
                "MobilePay.MerchantFeesCalculator.BasicImplementation.dll");

            // generate application objects from implementing assembly
            var source = factory.CreateTextBasedTransactionSource(transactionFilePath);
            var sourceFormat = factory.CreateTransactionInputFormat();
            var outputWriter = factory.CreateOutputWriter();
            var outputFormatter = factory.CreateOutputFormatter();

            // TODO: the fixed 29DKK fee could be refactored into separate object to and implemented
            // TODO: in more elegant fashion as discounts below
            var feeCalculator = factory.CreateTransactionFeeCalculator(29);

            // apply discounts to major companies
            context.ApplyDiscount("TELIA", factory.CreatePercentageDiscount(10));
            context.ApplyDiscount("CIRCLE_K", factory.CreatePercentageDiscount(20));

            // fetch transactions - lazy load
            foreach (var transaction in source.FetchTransactions(sourceFormat, context))
            {
                // log transaction into the system
                context.LogTransaction(transaction);

                // compute the fee
                var fee = feeCalculator.Compute(transaction, context);

                // output
                outputWriter.Write(fee, outputFormatter);
            }

            Console.ReadKey();
        }
    }
}

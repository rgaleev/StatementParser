using System;
using System.Collections.Generic;
using System.Linq;
using StatementParser.Attributes;
using StatementParser.Models;

namespace TaxReporterCLI.Models.Views
{
    public class StockCurrencySummaryView
    {
        [Description("Exchanged to currency")] public Currency ExchangedToCurrency { get; }

        public Currency Currency { get; }

        [Description("Total Income")] public decimal CombinedIncome { get; }

        [Description("Total Income in {ExchangedToCurrency} per Day")]
        public decimal ExchangedPerDayCombinedIncome { get; }

        [Description("Total Income in {ExchangedToCurrency} per Year")]
        public decimal ExchangedPerYearCombinedIncome { get; }

        public StockCurrencySummaryView(IList<DepositTransactionView> transactions, Currency currency)
        {
            this.Currency = currency;

            var currencyTransactions = transactions.Where(i => i.Transaction.Currency == currency);

            foreach (var transaction in currencyTransactions)
            {
                ExchangedToCurrency = currencyTransactions.First().ExchangedToCurrency;
                var saleTransaction = (transaction.Transaction as DepositTransaction);

                CombinedIncome += saleTransaction.TotalPrice;

                if (transaction.ExchangedPerDayTotalPrice.HasValue)
                {
                    ExchangedPerDayCombinedIncome += transaction.ExchangedPerDayTotalPrice.Value;
                }

                if (transaction.ExchangedPerYearTotalPrice.HasValue)
                {
                    ExchangedPerYearCombinedIncome += transaction.ExchangedPerYearTotalPrice.Value;
                }
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(ExchangedToCurrency)}: {ExchangedToCurrency} {nameof(Currency)}: {Currency} {nameof(CombinedIncome)}: {CombinedIncome} {nameof(ExchangedPerDayCombinedIncome)}: {ExchangedPerDayCombinedIncome} {nameof(ExchangedPerYearCombinedIncome)}: {ExchangedPerYearCombinedIncome}";
        }
    }
}
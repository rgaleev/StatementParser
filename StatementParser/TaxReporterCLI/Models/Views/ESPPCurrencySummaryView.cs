using System;
using System.Collections.Generic;
using System.Linq;
using StatementParser.Attributes;
using StatementParser.Models;

namespace TaxReporterCLI.Models.Views
{
    public class ESPPCurrencySummaryView
    {
        [Description("Exchanged to currency")] public Currency ExchangedToCurrency { get; }

        public Currency Currency { get; }

        [Description("Total Income")] public decimal CombinedProfit { get; }

        [Description("Total Income in {ExchangedToCurrency} per Day")]
        public decimal ExchangedPerDayCombinedIncome { get; }

        [Description("Total Income in {ExchangedToCurrency} per Year")]
        public decimal ExchangedPerYearCombinedIncome { get; }

        public ESPPCurrencySummaryView(IList<ESPPTransactionView> transactions, Currency currency)
        {
            this.Currency = currency;

            var currencyTransactions = transactions.Where(i => i.Transaction.Currency == currency);

            foreach (var transaction in currencyTransactions)
            {
                ExchangedToCurrency = currencyTransactions.First().ExchangedToCurrency;
                var saleTransaction = (transaction.Transaction as ESPPTransaction);

                CombinedProfit += saleTransaction.TotalProfit;

                if (transaction.ExchangedPerDayTotalProfit.HasValue)
                {
                    ExchangedPerDayCombinedIncome += transaction.ExchangedPerDayTotalProfit.Value;
                }

                if (transaction.ExchangedPerYearTotalProfit.HasValue)
                {
                    ExchangedPerYearCombinedIncome += transaction.ExchangedPerYearTotalProfit.Value;
                }
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(ExchangedToCurrency)}: {ExchangedToCurrency} {nameof(Currency)}: {Currency} {nameof(CombinedProfit)}: {CombinedProfit} {nameof(ExchangedPerDayCombinedIncome)}: {ExchangedPerDayCombinedIncome} {nameof(ExchangedPerYearCombinedIncome)}: {ExchangedPerYearCombinedIncome}";
        }
    }
}
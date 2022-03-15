using ASoft.TextDeserializer.Attributes;

namespace StatementParser.Parsers.Brokers.Revolut.PdfModels
{
    [DeserializeByRegex("Account number(?<AccountNumber>[^ ]+\\d)")]
    internal class StatementModel
    {
        public string AccountNumber { get; set; }

        [DeserializeCollectionByRegex(
            "(\\d{4}-\\d{2}-\\d{2})",
            "DividendsDateSymbolDividendWithholding TaxAmount(.+?)Total")]
        public DividendModel[] Dividends { get; set; }

        [DeserializeCollectionByRegex(
            "(\\d{4}-\\d{2}-\\d{2}\\d{4}-\\d{2}-\\d{2})",
            "Date acquiredDate soldSymbolQuantityCost basisGross ProceedsPnL.([\\s\\S]*)")]
        public SaleModel[] Sales { get; set; }
    }
}

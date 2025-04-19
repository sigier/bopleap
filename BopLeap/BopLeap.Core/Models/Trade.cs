using System.Text.Json.Serialization;
using BopLeap.Core.Attributes;


namespace BopLeap.Core.Models
{
    [InfluxMeasurement("trades")]
    public class Trade
    {
        [JsonPropertyName("e")]
        public string EventType { get; set; }

        [JsonPropertyName("E")]
        [InfluxProperty(InfluxPropertyKind.Timestamp)]
        public long EventTime { get; set; }

        [JsonPropertyName("s")]
        [InfluxProperty(InfluxPropertyKind.Tag)]
        public string Symbol { get; set; }

        [JsonPropertyName("t")]
        [InfluxProperty(InfluxPropertyKind.Field)]
        public long TradeId { get; set; }

        [JsonPropertyName("p")]
        [InfluxProperty(InfluxPropertyKind.Field)]
        public string Price { get; set; }

        [JsonPropertyName("q")]
        [InfluxProperty(InfluxPropertyKind.Field)]
        public string Quantity { get; set; }

        [JsonPropertyName("T")]
        public long TradeTime { get; set; }

        [JsonPropertyName("m")]
        public bool IsBuyerMarketMaker { get; set; }
    }
}

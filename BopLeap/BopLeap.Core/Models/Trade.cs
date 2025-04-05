using MessagePack;


namespace BopLeap.Core.Models
{
    [MessagePackObject]
    public class Trade
    {
        [Key("e")]
        public string EventType { get; set; }

        [Key("E")]
        public long EventTime { get; set; }

        [Key("s")]
        public string Symbol { get; set; }

        [Key("t")]
        public long TradeId { get; set; }

        [Key("p")]
        public string Price { get; set; }

        [Key("q")]
        public string Quantity { get; set; }

        [Key("T")]
        public long TradeTime { get; set; }

        [Key("m")]
        public bool IsBuyerMarketMaker { get; set; }
    }

}

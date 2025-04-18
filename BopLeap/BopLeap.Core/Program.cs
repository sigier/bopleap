using BopLeap.Core;

var client = new BinanceWebSocketClient();
await client.StartAsync(trade =>
{
    Console.WriteLine($"[TRADE] {trade.Symbol}: {trade.Price}");
});
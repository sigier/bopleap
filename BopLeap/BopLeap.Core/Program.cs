using BopLeap.Core;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

var wsClient = new BinanceWebSocketClient();

await wsClient.StartAsync(trade =>
{
    Console.WriteLine($"{trade.Symbol} @ {trade.Price} ({trade.Quantity})");
}, cts.Token);

Console.WriteLine("Exiting BopLeap.");
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using BopLeap.Core.Models;
using MessagePack;

namespace BopLeap.Core;

internal class BinanceWebSocketClient
{
    private const string BinanceWsUrl =
        "wss://stream.binance.com:9443/ws/btcusdt@trade";

    private readonly ClientWebSocket _webSocket = new();

    public async Task StartAsync(Action<Trade> onTradeReceived, CancellationToken cancellationToken = default)
    {
        await _webSocket.ConnectAsync(new Uri(BinanceWsUrl), cancellationToken);

        var buffer = new byte[8192];

        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
            if (result.MessageType == WebSocketMessageType.Close) break;

            var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

            try
            {
                using var doc = JsonDocument.Parse(json);
                var data = doc.RootElement.GetProperty("data").GetRawText();
                var binary = Convert.FromBase64String(data);
                var trade = MessagePackSerializer.Deserialize<Trade>(binary);
                onTradeReceived(trade);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Parse error: {ex.Message}");
            }
        }

        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", cancellationToken);
    }
}
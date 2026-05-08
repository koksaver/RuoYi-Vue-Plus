using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace RuoYi.Common.WebSocket
{
    public class WebSocketHandler
    {
        private static readonly ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> _connections = new();

        public async Task HandleAsync(System.Net.WebSockets.WebSocket webSocket, string? userId = null)
        {
            var connectionId = userId ?? Guid.NewGuid().ToString();
            _connections.TryAdd(connectionId, webSocket);

            try
            {
                var buffer = new byte[1024 * 4];
                while (webSocket.State == WebSocketState.Open)
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                        break;
                    }

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        await ProcessMessageAsync(connectionId, message);
                    }
                }
            }
            finally
            {
                _connections.TryRemove(connectionId, out _);
            }
        }

        private async Task ProcessMessageAsync(string connectionId, string message)
        {
            // Process incoming websocket messages
            var response = JsonSerializer.Serialize(new { type = "echo", data = message });
            await SendToClientAsync(connectionId, response);
        }

        public async Task SendToClientAsync(string connectionId, string message)
        {
            if (_connections.TryGetValue(connectionId, out var webSocket) && webSocket.State == WebSocketState.Open)
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task BroadcastAsync(string message)
        {
            var tasks = _connections.Where(c => c.Value.State == WebSocketState.Open)
                .Select(c => SendToClientAsync(c.Key, message));

            await Task.WhenAll(tasks);
        }

        public int GetConnectionCount()
        {
            return _connections.Count;
        }
    }
}
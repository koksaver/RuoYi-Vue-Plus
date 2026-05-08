using System.Collections.Concurrent;
using System.Text.Json;

namespace RuoYi.Common.Sse
{
    public class SseService
    {
        private static readonly ConcurrentDictionary<string, StreamWriter> _clients = new();

        public async Task AddClientAsync(string clientId, HttpResponse response)
        {
            response.Headers.Append("Content-Type", "text/event-stream");
            response.Headers.Append("Cache-Control", "no-cache");
            response.Headers.Append("Connection", "keep-alive");

            var writer = new StreamWriter(response.Body);
            _clients.TryAdd(clientId, writer);

            try
            {
                await writer.WriteAsync("data: connected\n\n");
                await writer.FlushAsync();

                // Keep connection alive
                while (!response.HttpContext.RequestAborted.IsCancellationRequested)
                {
                    await Task.Delay(30000, response.HttpContext.RequestAborted);
                    await writer.WriteAsync(": heartbeat\n\n");
                    await writer.FlushAsync();
                }
            }
            catch (OperationCanceledException) { }
            finally
            {
                _clients.TryRemove(clientId, out _);
                await writer.DisposeAsync();
            }
        }

        public async Task SendToClientAsync(string clientId, string eventType, object data)
        {
            if (_clients.TryGetValue(clientId, out var writer))
            {
                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await writer.WriteAsync($"event: {eventType}\n");
                await writer.WriteAsync($"data: {json}\n\n");
                await writer.FlushAsync();
            }
        }

        public async Task BroadcastAsync(string eventType, object data)
        {
            var tasks = _clients.Select(c => SendToClientAsync(c.Key, eventType, data));
            await Task.WhenAll(tasks);
        }

        public int GetClientCount()
        {
            return _clients.Count;
        }
    }
}
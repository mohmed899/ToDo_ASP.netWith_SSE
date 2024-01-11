using System.Text;
namespace task_sr_2.Services
{
    public class SseService
    {
        private readonly List<HttpContext> _clients = new List<HttpContext>();

        public void AddClient(HttpContext context)
        {
            _clients.Add(context);
        }

        public void RemoveClient(HttpContext context)
        {
            _clients.Remove(context);
        }

        public async Task SendSseMessageAsync(string message)
        {
            var data = Encoding.UTF8.GetBytes($"data: {message}\n\n");

            foreach (var client in _clients)
            {
                try
                {
                    await client.Response.Body.WriteAsync(data, 0, data.Length);
                    await client.Response.Body.FlushAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}





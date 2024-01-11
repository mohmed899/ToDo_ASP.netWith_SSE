using Microsoft.AspNetCore.Mvc;
using System.Text;
using task_sr_2.Services;


namespace task_sr_2.Controllers
{
    [Route("api/sse")]
    public class SseController : ControllerBase
    {
        private readonly SseService _sseService;

        public SseController(SseService sseService)
        {
            _sseService = sseService;
        }

        [HttpGet]
        public async Task get()
        {
            try
            {
                var context = HttpContext;

                Response.Headers.Add("Content-Type", "text/event-stream");
                Response.Headers.Add("Cache-Control", "no-cache");
                Response.Headers.Add("Connection", "keep-alive");

                _sseService.AddClient(context);

                var message = "data: Update from server\n\n";
                var messageBytes = Encoding.UTF8.GetBytes(message);
                await Response.Body.WriteAsync(messageBytes, 0, messageBytes.Length);
                await Response.Body.FlushAsync();
                while (!HttpContext.RequestAborted.IsCancellationRequested)
                {
                    //  must keep the connection open 
                    Console.WriteLine(HttpContext.RequestAborted.IsCancellationRequested);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}




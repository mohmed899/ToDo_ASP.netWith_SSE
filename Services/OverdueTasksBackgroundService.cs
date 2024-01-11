
using Microsoft.AspNetCore.Http;
using task_sr_2.Models;

namespace task_sr_2.Services
{
    public class OverdueTasksBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly SseService _sseService;
        public OverdueTasksBackgroundService(IServiceScopeFactory scopeFactory, SseService sseService)
        {
            _scopeFactory = scopeFactory;
            _sseService = sseService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DiaryDBContext>();

                    var overdueTasks = dbContext.DiaryEntries
                        .Where(e => e.DateTime < DateTime.Now && e.Status !=2  )  // 0 new 1-recent 2-overdue
                        .ToList();
                    var message = "";
                    foreach (var task in overdueTasks)
                    {
                       task.Status = 2;
                        dbContext.SaveChanges();
                        message += $"{task.Id},";// send the expired Task 
                    }if(!String.IsNullOrEmpty(message))
                      await _sseService.SendSseMessageAsync(message);
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}



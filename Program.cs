using Microsoft.EntityFrameworkCore;
using task_sr_2.Models;
using task_sr_2.Repo;
using task_sr_2.Services;

namespace task_sr_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var allowedOrigines = "myOri";
            // Add services to the container.
            builder.Services.AddDbContext<DiaryDBContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            builder.Services.AddCors(op =>
            {
                op.AddPolicy(allowedOrigines, po =>
                {
                    po.AllowAnyMethod();
                    po.AllowAnyHeader();
                    po.AllowAnyOrigin();
                });
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDiaryRepository, DiaryRepository>();
            builder.Services.AddHostedService<OverdueTasksBackgroundService>();
            builder.Services.AddSingleton<SseService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
         

            app.UseAuthorization();
            app.UseCors(allowedOrigines);
            app.MapControllers();

            app.Run();
        }
    }
}
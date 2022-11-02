using EmailService.Services;
using Serilog;
using EmailService.Models;
using Microsoft.EntityFrameworkCore;
using Dapr;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/EmailService.text", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
// Add services to the container.
var serviceProvider = builder.Services.BuildServiceProvider();

builder.Services.AddSingleton<IBetterMailService, BetterMailService>();
builder.Services.AddScoped<IEmailLogRepository, EmailLogRepository>();
builder.Services.AddDbContext<DIEmailContext>(options => options.UseSqlServer(
       builder.Configuration["ConnectionStrings:DefaultConnection"]
       ));

builder.Services.AddDaprClient();
builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCloudEvents();
app.UseAuthorization();
app.MapControllers();
app.MapSubscribeHandler();
app.Run();

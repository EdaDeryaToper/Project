using ExceptionHandler;
using Microsoft.AspNetCore.Localization;
using NLog;
using ProductService.Data;
using ProductService.Extensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "\\nlog.config"));

// Add services to the container.
builder.Services.AddServices(builder.Configuration);
builder.Services.ConfigureLoggerManager();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
if (app.Environment.IsProduction())
{
    app.UseHsts();
}
app.ConfigureExceptionHandler(logger);
try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{

    Console.WriteLine(e);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

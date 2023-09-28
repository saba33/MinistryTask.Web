using Microsoft.EntityFrameworkCore;
using MinistryTask.Repository.DatabaseContext;
using MinistryTask.Web.ExtensionMethods;
using MinistryTask.Web.Middlewares;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddDependencies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#pragma warning disable CS0618 
Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Verbose()
      .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
      .Enrich.FromLogContext()
      .WriteTo.MSSqlServer(
          connectionString: builder.Configuration.GetConnectionString("connectionString"),
          tableName: "Logs",
          autoCreateSqlTable: true,
          columnOptions: new ColumnOptions(),
          restrictedToMinimumLevel: LogEventLevel.Verbose)
      .CreateLogger();
#pragma warning restore CS0618
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using MinistryTask.Domain.Abstractions;
using MinistryTask.Repository.DatabaseContext;
using MinistryTask.Repository.Repositories;
using MinistryTask.Serivices.Abstraction;
using MinistryTask.Serivices.Implementation;
using MinistryTask.Web.Middlewares;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
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

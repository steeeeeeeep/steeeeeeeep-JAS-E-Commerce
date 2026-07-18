using JAS.ECommerce.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog configuration
builder.Host.UseSerilog((context, configuration) =>
    configuration
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day));

// Add services to the container
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();

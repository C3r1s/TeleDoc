using Microsoft.OpenApi.Models;
using Serilog;
using TeleDoc.API.Extensions;
using TeleDoc.API.Filters;
using TeleDoc.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDataServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.ConfigureApiServices();
builder.Services.AddAuthorization();

builder.Services.AddControllers(options => 
{
    options.Filters.Add<CustomExceptionFilter>(); 
    options.Filters.Add<StandardResultFilter>();

});
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "TeleDoc API", Version = "v1" }); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeleDoc API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting web application");
    using var scope = app.Services.CreateScope();
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
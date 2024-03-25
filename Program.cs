using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "weather-app";


builder.Services.AddOpenTelemetry()
    
      .WithMetrics(x =>
      {
          x.AddPrometheusExporter();

          x.AddMeter("Microsoft.AspNetCore.Hosting",
             "Microsoft.AspNetCore.Server.Kestrel",
             "System.Net.Http");
          x.AddView("request-duration",
           new ExplicitBucketHistogramConfiguration
           {
               Boundaries = new[] { 0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75 }
           });

      })
      .WithTracing(b =>
      {
          if (builder.Environment.IsDevelopment())
          {
              b.SetSampler<AlwaysOnSampler>();
          }
          b.AddConsoleExporter()
            .AddSource(serviceName)
            .SetResourceBuilder(
              ResourceBuilder.CreateDefault().AddService(serviceName))
          .AddAspNetCoreInstrumentation()
          .AddAspNetCoreInstrumentation()
          .AddHttpClientInstrumentation();

      });
    

  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPrometheusScrapingEndpoint();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

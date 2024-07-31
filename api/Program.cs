var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapFallback(() =>
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
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => (int)Math.Round(32.0 + (9.0 / 5.0 * TemperatureC));
}

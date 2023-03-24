using Common.Middleware;
using Projects.API.Extansions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureRepositories(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureHttpClient();
builder.Services.AddResponseCaching();

var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();
app.UseResponseCaching();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.SeedData(builder.Configuration);

app.Run();

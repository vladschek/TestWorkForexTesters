using Common.Middleware;
using Users.API.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.ConfigureServices();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureHttpClient();

var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MakeMigration(builder.Configuration);

app.Run();


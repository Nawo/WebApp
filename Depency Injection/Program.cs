using Depency_Injection;
using Depency_Injection.Infrastructure;
using Depency_Injection.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection"]);
});

var app = builder.Build();

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.MapGet("/cookie", async context =>
{
    int counter = int.Parse(context.Request.Cookies["counter"] ?? "0") + 1;

    context.Response.Cookies.Append(
        "counter",
        counter.ToString(),
        new CookieOptions
        {
            MaxAge = TimeSpan.FromMinutes(30)
        });
    await context.Response.WriteAsync($"Cookie: {counter}");
});

app.MapGet("/clear", context =>
{
    context.Response.Cookies.Delete("counter");
    context.Response.Redirect("/");
    return Task.CompletedTask;
});

app.MapGet("/", () => "Hello!");

app.Run();
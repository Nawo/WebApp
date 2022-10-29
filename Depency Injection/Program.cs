using Depency_Injection;
using Depency_Injection.Services;

var builder = WebApplication.CreateBuilder(args);

var servicesConfig = builder.Configuration;
builder.Services.Configure<FruitOptions>(servicesConfig.GetSection("Fruit")); //pobranie konfiguracji z appsettings

builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();

var app = builder.Build();

//IResponseFormatter formatter = new TextResponseFormatter();

app.MapGet("/format1", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Formatter 1");
});

app.MapGet("/format2", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Formatter 2");
});

app.UseMiddleware<CustomMiddleware>();

app.UseMiddleware<FruitMiddleware>();

app.MapGet("/config", async (HttpContext context, IConfiguration config) =>
{
    string defaultDebug = config["Logging:LogLevel:Default"];
    await context.Response.WriteAsync(defaultDebug);

    string environment = config["ASPNETCORE_ENVIRONMENT"];
    await context.Response.WriteAsync(environment);

    if(app.Environment.IsDevelopment())
    {
        await context.Response.WriteAsync("Is Development");
    }
    else
    {
        await context.Response.WriteAsync("Is not Development");
    }
});

app.MapGet("/endpoint", CustomEndpoint.Endpoint);

app.MapGet("/", () => "Hello!");

app.UseStaticFiles();

app.Run();
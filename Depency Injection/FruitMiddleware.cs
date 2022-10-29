using Microsoft.Extensions.Options;

namespace Depency_Injection;

public class FruitMiddleware
{
    private RequestDelegate _next;
    private FruitOptions _options;

    public FruitMiddleware()
    {

    }

    public FruitMiddleware(RequestDelegate next, IOptions<FruitOptions> options)
    {
        _next = next;
        _options = options.Value;
    }

    public async Task Invoke(HttpContext context, ILogger<FruitMiddleware> logger)
    {
        if (context.Request.Path == "/fruit")
        {
            logger.LogDebug($"Started processing for {context.Request.Path}");
            await context.Response.WriteAsync($"{_options.Name}, {_options.Color}");
            logger.LogDebug($"End processing for {context.Request.Path}");
        }

        else
        {
            await _next(context);
        }

    }

}

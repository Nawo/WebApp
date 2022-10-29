using Depency_Injection.Services;

namespace Depency_Injection;

public class CustomMiddleware
{

    private RequestDelegate _next;
    private IResponseFormatter _formatter;
    public CustomMiddleware(RequestDelegate next, IResponseFormatter formatter)
    {
        _next = next;
        _formatter = formatter;
    }

    public async Task Invoke(HttpContext context)
    {
        if(context.Request.Path == "/Middleware")
        {
            await _formatter.Format(context, "Custom Middleware");
        }
        else
        {
            await _next(context);
        }
    }
}

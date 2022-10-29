using Depency_Injection.Services;


namespace Depency_Injection;

public class CustomEndpoint
{
    public static async Task Endpoint(HttpContext context, IResponseFormatter formatter)
    {
        await formatter.Format(context, "Custom Endpoint");
    }
}

﻿namespace Depency_Injection.Services;

public interface IResponseFormatter
{

    Task Format(HttpContext context, string content);
}

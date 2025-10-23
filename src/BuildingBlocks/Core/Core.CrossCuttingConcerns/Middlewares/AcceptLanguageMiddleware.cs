using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Middlewares;

public class AcceptLanguageMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext? context)
    {
        if (context != null)
        {
            var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();
            var culture = string.IsNullOrWhiteSpace(acceptLanguage) ? "en" : acceptLanguage.Split(',')[0];
            try
            {
                var cultureInfo = new CultureInfo(culture);
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;
            }
            catch (CultureNotFoundException)
            {
                var defaultCulture = new CultureInfo("en");
                CultureInfo.CurrentCulture = defaultCulture;
                CultureInfo.CurrentUICulture = defaultCulture;
            }
        }

        if (context != null) await next(context);
    }
}
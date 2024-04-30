using System.Globalization;

namespace PersonDirectory.Api.Middlewares
{
    public class AcceptLanguageHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public AcceptLanguageHeaderMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            var culture = context.Request.Headers["Accept-Language"].ToString();

            var cultures = new[] { "en-US", "ka-GE" };

            if (cultures.Contains(culture))
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                CultureInfo.CurrentUICulture = new CultureInfo(culture);
            }

            await _next(context);
        }
    }
}

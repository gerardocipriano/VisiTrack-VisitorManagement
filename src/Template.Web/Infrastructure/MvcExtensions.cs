using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Template.Web.Infrastructure
{
    public static class MvcExtensions
    {
        public static CultureInfo CurrentCulture(this RazorPage page)
        {
            var rqf = page.Context.Features.Get<IRequestCultureFeature>();
            return rqf.RequestCulture.Culture;
        }

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        private static string ACTIVE_CLASS = "active";

        /// <summary>
        /// Restituisce "active" se il nome dell'area,del controller e dell'action sono attualmente nel routing,
        /// viceversa restituisce String.Empty
        /// </summary>
        /// <param name="context"></param>
        /// <param name="areaName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionNames"></param>
        /// <returns></returns>
        public static string ActiveClassIf(this ViewContext context, string areaName, string controllerName, params string[] actionNames)
        {
            if ((areaName == null || string.Equals(areaName, context.RouteData.Values["Area"]?.ToString(), System.StringComparison.OrdinalIgnoreCase)) &&
                (controllerName == null || string.Equals(controllerName, context.RouteData.Values["Controller"]?.ToString(), System.StringComparison.OrdinalIgnoreCase)) &&
                (actionNames == null || actionNames.Any(x => string.Equals(x, context.RouteData.Values["Action"]?.ToString(), System.StringComparison.OrdinalIgnoreCase))))
                return ACTIVE_CLASS;
            return string.Empty;
        }
    }
}
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace YomiOlatunji.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string IsSelecteda(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (string.IsNullOrEmpty(cssClass)) cssClass = "active";
            var currentAction = (string)(html.ViewContext?.RouteData?.Values["action"]??"");
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];
            var areaController = (string)html.ViewContext.RouteData.Values["area"];
            if (string.IsNullOrEmpty(controller)) controller = currentController;
            if (string.IsNullOrEmpty(action)) action = currentAction;
            return controller == currentController && action == currentAction ? cssClass : string.Empty;
        }
    }
}

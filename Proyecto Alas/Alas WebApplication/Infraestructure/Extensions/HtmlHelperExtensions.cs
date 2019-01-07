using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AlasWebApplication.Infraestructure.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string Id(this IHtmlHelper htmlHelper)
        {
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("id"))
            {
                return (string)routeValues["id"];
            }
            else if (System.Web.HttpContext.Current.Request.Query.ContainsKey("id"))
            {
                return HttpContext.Current.Request.Query["id"];
            }

            return string.Empty;
        }

        public static string Controller(this IHtmlHelper htmlHelper)
        {
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
            {
                return (string)routeValues["controller"];
            }

            return string.Empty;
        }

        public static string Action(this IHtmlHelper htmlHelper)
        {
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
            {
                return (string)routeValues["action"];
            }

            return string.Empty;
        }

        public static string Area(this IHtmlHelper htmlHelper)
        {
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("area"))
            {
                return (string)routeValues["area"];
            }

            return string.Empty;
        }

        public static string Page(this IHtmlHelper htmlHelper)
        {
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("page"))
            {
                return (string)routeValues["page"];
            }

            return string.Empty;
        }
    }
}

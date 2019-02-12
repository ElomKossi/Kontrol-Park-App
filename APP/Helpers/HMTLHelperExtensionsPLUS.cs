using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP
{
    public static class HMTLHelperExtensionsPLUS
    {
        public static string IsSelectedPLUS(this HtmlHelper html, string area = null, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            string currentArea = (string)html.ViewContext.RouteData.Values ["area"];

            if (String.IsNullOrEmpty(area))
                area = currentArea;

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return area == currentArea && controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        //public static string PageClass(this HtmlHelper html)
        //{
        //    string currentAction = (string)html.ViewContext.RouteData.Values["action"];
        //    return currentAction;
        //}
    }
}
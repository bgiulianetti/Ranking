using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUA.Extension
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, string controller, object routeValues, string imagePath)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            //imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.MergeAttribute("style", "height:20px");
            
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, controller, routeValues));
            if(action == "DeleteStandupero")
            {
                imgBuilder.MergeAttribute("class", "confirm");
                anchorBuilder.MergeAttribute("href", url.Action(action, controller, routeValues));
            }

            if (action == "PrintBordereaux")
            {
                anchorBuilder.MergeAttribute("target", "_blank");
            }

            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }
    }
}
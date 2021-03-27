using System;
using System.Collections.Generic;
using System.Xml.Linq;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BowlingLeague.Infrastructure
{
    //This whole class is developed to create a div and a tag to
    //put in the view to allow users to view different page items
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelper;

        public PaginationTagHelper(IUrlHelperFactory urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public int i = 0;

        public PageNumbering PageInfo { get; set; }

        //Creates a dictionary to store url information about the
        //team name and page number
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        public bool PageClassesEnabled { get; set; } = false;

        public string PageClass { get; set; }

        public string PageClassNormal { get; set; }

        public string PageClassSelected { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext {get; set;}

        //This method is tasked with building the tag
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelp = _urlHelper.GetUrlHelper(ViewContext);

            TagBuilder finishedTag = new TagBuilder("div");

            for (i = 1; i <= PageInfo.NumPages; i++)
            {
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = i;
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);

                //This is used to allow for CSS to be used
                if (PageClassesEnabled)
                {
                    individualTag.AddCssClass(PageClass);
                    individualTag.AddCssClass(i == PageInfo.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                individualTag.InnerHtml.Append(i.ToString());

                finishedTag.InnerHtml.AppendHtml(individualTag);
            }

            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}

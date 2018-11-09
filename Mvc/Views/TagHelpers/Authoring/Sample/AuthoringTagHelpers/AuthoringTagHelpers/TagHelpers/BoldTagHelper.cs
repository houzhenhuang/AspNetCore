using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AuthoringTagHelpers.TagHelpers
{
    /// <summary>
    /// RemoveAll、PreContent.SetHtmlContent 和 PostContent.SetHtmlContent
    /// </summary>
    [HtmlTargetElement(Attributes ="bold")]
    [HtmlTargetElement("bold")]
    public class BoldTagHelper:TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("bold");
            output.PreContent.SetHtmlContent("<strong>");
            output.PostContent.SetHtmlContent("</strong>");
        }
    }
}

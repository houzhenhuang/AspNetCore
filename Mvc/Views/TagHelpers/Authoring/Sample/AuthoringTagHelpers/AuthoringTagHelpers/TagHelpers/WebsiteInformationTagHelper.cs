using AuthoringTagHelpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AuthoringTagHelpers.TagHelpers
{
    /// <summary>
    /// 将模型传递到标记帮助程序
    /// </summary>
    public class WebsiteInformationTagHelper:TagHelper
    {
        public WebsiteContext Info { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.Content.SetHtmlContent($@"<ul>
                                             <li><strong>Version:</strong> {Info.Version}</li>
                                             <li><strong>Copyright Year:</strong> {Info.CopyrightYear}</li>
                                             <li><strong>Approved:</strong> {Info.Approved}</li>
                                             <li><strong>Number of tags to show:</strong> {Info.TagsToShow}</li>
                                             </ul>");
            output.TagMode = TagMode.StartTagAndEndTag;
        }

    }
}

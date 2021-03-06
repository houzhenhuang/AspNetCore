﻿#define GetContent
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthoringTagHelpers.TagHelpers
{
    /// <summary>
    /// 最小的标记帮助程序
    /// </summary>
    public class EmailTagHelper : TagHelper
    {
#if MailTo
        private const string EmailDomain = "contoso.com";

        public string MailTo { get; set; }
        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            var address = MailTo + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + address);
            output.Content.SetContent(address);
            await base.ProcessAsync(context, output);
        }
#elif GetContent
        private const string EmailDomain = "contoso.com";
        /// <summary>
        /// SetAttribute 和 SetContent
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var content = await output.GetChildContentAsync();// content of the get label
            var address = content.GetContent() + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + address);
            output.Content.SetContent(address);
        }
#endif
    }
}

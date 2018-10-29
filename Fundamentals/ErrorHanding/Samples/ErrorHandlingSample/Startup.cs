#define StatusCodePagesWithRedirect
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorHandlingSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
#if DeveloperExceptionPage
            #region DeveloperExceptionPage

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            #endregion
#elif StatusCodePages
            #region StatusCodePages
            app.UseStatusCodePages(async context=> {
                context.HttpContext.Response.ContentType = "text/plain";

                await context.HttpContext.Response.WriteAsync(
                    "Status code page, status code: " +
                    context.HttpContext.Response.StatusCode);
            });
            #endregion
#elif StatusCodePagesWithRedirect
            #region StatusCodePagesWithRedirect
             app.UseStatusCodePagesWithRedirects("/error/{0}");
            #endregion
#endif
            app.MapWhen(context => context.Request.Path == "/missingpage", builder => { });

            app.Map("/error", error =>
            {
                error.Run(async context =>
                {
                    var builder = new StringBuilder();
                    builder.AppendLine("<html><body>");
                    builder.AppendLine("An error occurred.<br />");
                    var path = context.Request.Path.ToString();
                    if (path.Length > 1)
                    {
                        builder.AppendLine("Status Code: " +
                            HtmlEncoder.Default.Encode(path.Substring(1)) + "<br />");
                    }
                    var referrer = context.Request.Headers["referer"];
                    if (!string.IsNullOrEmpty(referrer))
                    {
                        builder.AppendLine("Return to <a href=\"" +
                            HtmlEncoder.Default.Encode(referrer) + "\">" +
                            WebUtility.HtmlEncode(referrer) + "</a><br />");
                    }
                    builder.AppendLine("</body></html>");
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync(builder.ToString());
                });
            });

            app.Run(async (context) =>
            {
                if (context.Request.Query.ContainsKey("throw"))
                {
                    throw new Exception("Exception triggered!");
                }
                var builder = new StringBuilder();
                builder.AppendLine("<html><body>Hello World!");
                builder.AppendLine("<ul>");
                builder.AppendLine("<li><a href=\"/?throw=true\">Throw Exception</a></li>");
                builder.AppendLine("<li><a href=\"/missingpage\">Missing Page</a></li>");
                builder.AppendLine("</ul>");
                builder.AppendLine("</body></html>");

                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(builder.ToString());
            });
        }
    }
}

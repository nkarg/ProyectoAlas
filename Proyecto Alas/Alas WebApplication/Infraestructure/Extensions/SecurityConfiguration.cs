using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlasWebApplication.Infraestructure.Extensions
{
    public static class SecurityConfiguration
    {
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            //Registered before static files to always set header

            //Strict-Transport-Security
            app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());
            //X-Content-Type-Options
            app.UseXContentTypeOptions();
            //Referrer-Policy
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            //Web Browser XSS Protection
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            //X-Frame-Options Header
            app.UseXfo(options => options.Deny());

            //Content-Security-Policy Header (Disabled)
            //app.UseCsp(opts => opts
            //    .BlockAllMixedContent()
            //    .StyleSources(s => s.Self())
            //    .StyleSources(s => s.UnsafeInline())
            //    .FontSources(s => s.Self())
            //    .FormActions(s => s.Self())
            //    .FrameAncestors(s => s.Self())
            //    .ImageSources(s => s.Self())
            //    .ScriptSources(s => s.Self())
            //);


            //Add Custom Headers
            //app.Use((context, next) =>
            //{
            //    context.Response.Headers.Add("MyHeader", string.Empty);
            //    return next();
            //});

            return app;
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlasWebApplication.Infraestructure.Filters
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, IHostingEnvironment environment, UserManager<IdentityUser> userManager)
        {
            _next = next;
            _environment = environment;
            _userManager = userManager;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var appPath = _environment.ContentRootPath;
                var rootPath = _environment.WebRootPath;
                var fileFolder = Path.Combine(appPath, "Logs");
                var fileName = $"{context.TraceIdentifier.Replace(":", "-")}-{DateTime.Today.ToString("dd-MM-yyyy")}.txt";

                var stackTrace = new StackTrace(ex, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = frame.GetFileName();
                var userId = context.User.Identity.IsAuthenticated ? _userManager.GetUserId(context.User) : "Not authenticated";

                Directory.CreateDirectory(fileFolder);
                using (StreamWriter sw = new StreamWriter(Path.Combine(fileFolder, fileName)))
                {
                    sw.WriteLine("Unhandled exception");
                    sw.WriteLine("-------------------");
                    sw.WriteLine($"User ID: {userId}");
                    sw.WriteLine($"Identifier: {context.TraceIdentifier}");
                    sw.WriteLine($"Registered on: {DateTime.Now}");
                    sw.WriteLine($"Exception message: {ex.Message}");
                    sw.WriteLine($"Request: {context.Request.Path.Value} (Method: {context.Request.Method})");
                    sw.WriteLine($"Exception on file: {file} at line {line}");
                    sw.WriteLine($"-------------------");
                    sw.WriteLine($"Complete StackTrace");
                    sw.WriteLine($"{ex.StackTrace}");
                }

                throw;
            }
        }
    }
}

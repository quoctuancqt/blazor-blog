using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace BlazorBlogApp2.Server.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

       
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"------Error: {ex.Message}--------");
            }
        }
    }
}

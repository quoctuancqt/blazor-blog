using BlazorBlogApp2.Server.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorBlogApp2.Server
{
    public static class CustomExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ToDoService>();

            services.AddScoped<AuthenticationService>();
        }
    }
}

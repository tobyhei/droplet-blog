using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace tobyheighwaydotcom
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            var database = new Database(app.ApplicationServices.GetRequiredService<ILogger<Database>>());
            var homePage = new HomePage(database);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", homePage.Get);
            });
        }
    }
}

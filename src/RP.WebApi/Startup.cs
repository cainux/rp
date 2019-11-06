using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using RP.Core.Repositories;
using RP.Core.Services;
using RP.Infrastructure.External;
using RP.Infrastructure.Repositories;

namespace RP.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<IPhotoRepository, PhotoRepository>();
            services.AddTransient<IIntegratorService, IntegratorService>();
            services.AddTransient(_ => RestService.For<IJsonPlaceHolderApi>("http://jsonplaceholder.typicode.com/"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

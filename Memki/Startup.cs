using FluentValidation;
using MediatR;
using Memki.AppStart;
using Memki.Common;
using Memki.Common.ExceptionHandling;
using Memki.Common.Validation;
using Memki.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Memki
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(configure => configure.Filters.Add(new AuthorizeFilter()));

            var connectionString = _configuration.GetConnectionString("Db");
            
            services.AddScoped(_ => new Context(connectionString));
            services.AddAuth();

            services.AddHttpContextAccessor();
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.Scan(scan => scan.FromAssemblies(typeof(Startup).Assembly)
                .AddClasses(c => c.AssignableTo(typeof(IExceptionHandler)))
                .AsImplementedInterfaces()
            );
            
            
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
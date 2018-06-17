using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechPushCoreApi.Data.Context;
using TechPushCoreApi.Data.Repositories.Implementations;
using TechPushCoreApi.Data.Repositories.Interfaces;
using TechPushCoreApi.Services.Entities;
using TechPushCoreApi.Services.Implementations;
using TechPushCoreApi.Services.Intefaces;

namespace TestProj
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var context = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(context));

            services.AddTransient<IAPPServices, APPServices>();
            services.AddTransient<IRegPushRepository, RegPushRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<IEntityMapper, EntityMapper>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IPushService, PushService>();
            services.AddTransient<ICipher, Cipher>();
            services.Configure<ServiceSettings>(Configuration.GetSection("ServiceSettings"));
            services.AddMvc();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

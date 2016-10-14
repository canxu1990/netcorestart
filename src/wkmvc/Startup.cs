using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using wkmvc.Data;
using Service.Cahce;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace wkmvc
{
    public class Startup
    {
        private readonly CacheProvider _cacheprovide;
        private readonly DataBaeProvider _dataBaseProvide;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
            _dataBaseProvide = new DataBaeProvider();
            _cacheprovide = new CacheProvider();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加服务框架.
            services.AddApplicationInsightsTelemetry(Configuration);
            if(_dataBaseProvide._isSqlServer)
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("connectionString")));
            if(_dataBaseProvide._isMysql)
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MySqlConnection")));
            services.AddMvc();
            services.AddSession();
            //services.AddCaching();
            services.AddTransient<Service.ConfigServices.AppConfigurtaionServices>();
            //注入UserMnage服务
            services.AddTransient<Service.IService.IUserManage, Service.ServiceImp.UserManage>();
            //添加工作单元
            services.AddTransient<Core.IUnitOfWork, Core.UnitOfWork>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            //添加系统配置
            services.Configure<ApplicationConfiguration>(Configuration.GetSection("ApplicationConfiguration"));
            //添加缓存服务
            services.AddMemoryCache();
            if (_cacheprovide._isUseRedis)
            {
                //使用redis
                services.AddSingleton(typeof(ICacheService), new RedisCacheService(new Microsoft.Extensions.Caching.Redis.RedisCacheOptions
                {
                    Configuration = _cacheprovide._connectionString,
                    InstanceName = _cacheprovide._instanceName
                }, 0));
            }
            else
            {
                //使用memorycache
                services.AddSingleton<IMemoryCache>(factory =>
                {
                    var cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
                    return cache;
                });
                services.AddSingleton<ICacheService, Service.Cahce.MemoryCache>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UserWkMvcDI();
            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("areaRoute", "{area:exists}/{controller}/{action=Index}/{id?}");
            });
        }
    }
}

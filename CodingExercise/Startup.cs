using AutoMapper;
using CodingExercise.AppLayer;
using CodingExercise.Helpers;
using CodingExercise.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace CodingExercise
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
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // configure strongly typed settings objects
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton(Configuration);
            var confFile = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            // Database config - SqLite
            services.AddDbContext<DataContext>(options => options.UseSqlite(confFile.AppConnSqLite));

            services.AddControllers()
                .AddFluentValidation(opt =>
                    {
                        opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    });

            // Register processing gateway
            services.AddScoped<IPmtService, PmtService>();

            services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddScoped<IProcessPmtGateway, ProcessPmtGateway>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

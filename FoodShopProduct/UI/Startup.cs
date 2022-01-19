using BLL;
using BLL.Implementation;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace UI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _currentEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region swagger

            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "UI", Version = "v1"}); });

            #endregion

            services.AddCors();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductInfoRepository, ProductInfoRepository>();
            services.AddSingleton(_ => AutoMapperProfile.InitializeAutoMapper().CreateMapper());
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductInfoService, ProductInfoService>();

            AddDb(services);
        }

        private void AddDb(IServiceCollection services)
        {
            //if (_currentEnvironment.IsEnvironment("Testing"))
            //    services.AddDbContext<ProductContext>(options =>
            //        options.UseInMemoryDatabase("TestDB"));
            //else
            services.AddDbContext<ProductContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
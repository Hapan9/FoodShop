using BLL;
using BLL.Implementation;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace UI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region swagger

            services.AddControllers();
            services
                .AddSwaggerGen(swagger =>
                {
                    //This is to generate the Default UI of Swagger Documentation  
                    swagger.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Forum",
                        Description = "InternetShop Auth"
                    });
                    // To Enable authorization using Swagger (JWT)  
                    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description =
                            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
                    });
                    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });
                });

            #endregion

            services.AddCors();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductInfoRepository, ProductInfoRepository>();
            services.AddSingleton(_ => AutoMapperProfile.InitializeAutoMapper().CreateMapper());
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductInfoService, ProductInfoService>();
            services.AddTransient<IProductScoreRepository, ProductScoreRepository>();
            services.AddTransient<IProductScoreService, ProductScoreService>();
            services.AddTransient<IAuthService, AuthService>();


            AddDb(services);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false
                    };
                });
        }

        private void AddDb(IServiceCollection services)
        {
            if (_webHostEnvironment.IsEnvironment("Testing"))
                services.AddDbContext<ProductContext>(options =>
                    options.UseInMemoryDatabase("TestProductDB"));
            else
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


            app.UseAuthentication();

            app.UseMiddleware<JwtMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                if (!env.IsProduction())
                {
                    endpoints.MapControllers().WithMetadata(new CustomAllowAnonymousAttribute());
                }
                else
                {
                    endpoints.MapControllers();
                }
            });
        }
    }
}
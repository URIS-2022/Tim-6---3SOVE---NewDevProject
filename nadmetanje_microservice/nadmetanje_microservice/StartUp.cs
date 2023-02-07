using nadmetanje_microserviceDLL.Context;
using Microsoft.EntityFrameworkCore;
using nadmetanje_microserviceBLL.Services.Interfaces;
using nadmetanje_microserviceBLL.Services.Implementations;
using nadmetanje_microserviceDAL.Repositories.Interfaces;
using nadmetanje_microserviceDAL.Repositories.Implementations;
using Microsoft.OpenApi.Models;
using nadmetanje_microserviceBLL.Mappings;
using Microsoft.OpenApi.Any;

namespace nadmetanje_microservice
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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddDbContext<NadmetanjeContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    sqlServerOptions.CommandTimeout(40000);
                }));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nadmetanje API", Version = "v1" });
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description =
                            "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference =
                                new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                c.CustomSchemaIds(type => $"{type.Name}_{System.Guid.NewGuid()}");
            });

            services.AddControllersWithViews();
            services.AddMvc();
            BindServices(services);
            AddMappings(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Nadmetanje API");
                    options.RoutePrefix = "apiDoc";
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddMappings(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EtapaProfile));
        }

        private void BindServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IEtapaService, EtapaService>();
            services.AddTransient<IEtapaRepository, EtapaRepository>();
        }
    }
}


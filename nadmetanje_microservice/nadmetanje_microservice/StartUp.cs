using nadmetanje_microserviceDLL.Context;
using Microsoft.EntityFrameworkCore;
using nadmetanje_microserviceBLL.Services.Interfaces;
using nadmetanje_microserviceBLL.Services.Implementations;
using nadmetanje_microserviceDAL.Repositories.Interfaces;
using nadmetanje_microserviceDAL.Repositories.Implementations;
using Microsoft.OpenApi.Models;
using nadmetanje_microserviceBLL.Mappings;
using Microsoft.OpenApi.Any;
using System.ComponentModel.Design;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

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

            services.AddDatabaseDeveloperPageExceptionFilter();

            var secret = Configuration["Jwt:Key"].ToString();
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddSwaggerGen(c => {
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nadmetanje sistema API",
                    Version = "v1",
                    Description = "Ovaj API odnosi se na nadmetanja sistema koji se implementira, i pruza osnovne CRUD kao i dodatne operacije.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Vukasin Stanisic IT26/2019",
                        Email = "vukasin.vs23@@gmail.com",
                        Url = new Uri(Configuration["Swagger:Github"])
                    }

                });
                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                c.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddDbContext<NadmetanjeContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    sqlServerOptions.CommandTimeout(40000);
                }));

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
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Nadmetanje API");
                    options.RoutePrefix = "swagger";
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddMappings(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EtapaProfile));
        }

        private static void BindServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IEtapaService, EtapaService>();
            services.AddTransient<IEtapaRepository, EtapaRepository>();
            
            services.AddTransient<INadmetanjeService, NadmetanjeService>();
            services.AddTransient<INadmetanjeRepository, NadmetanjeRepository>();

            services.AddTransient<IHttpService<double>, HttpService<double>>();
        }
    }
}


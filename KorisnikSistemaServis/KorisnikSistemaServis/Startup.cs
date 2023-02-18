//using KorisnikSistemaService.Auth;
/*using KorisnikSistemaService.Data;
using KorisnikSistemaService.Data.Interfaces;
using KorisnikSistemaService.Entities.DataContext;
using KorisnikSistemaService.ServiceCalls;*/
using KorisnikSistemaServis.AuthHelpers;
using KorisnikSistemaServis.Data;
using KorisnikSistemaServis.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;


namespace KorisnikSistemaService
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

            services.AddControllers(setup =>
                setup.ReturnHttpNotAcceptable = true
            ).AddXmlDataContractSerializerFormatters() 
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();

                    ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                        context.HttpContext,
                        context.ModelState);

                    problemDetails.Detail = "Pogledajte polje errors za detalje.";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    var actionExecutiongContext = context as ActionExecutingContext;

                    if ((context.ModelState.ErrorCount > 0) &&
                        (actionExecutiongContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                    {
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "Došlo je do greške prilikom validacije.";

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Došlo je do greške prilikom parsiranja poslatog sadržaja.";
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IAuthRepository>(new AuthRepository(secret));
            services.AddScoped<IKorisnikRepository, KorisnikRepository>();
            //services.AddSingleton<IKorisnikRepository, KorisnikMockRepository>();
            services.AddSwaggerGen(c =>
            {
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
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Korisnik sistema API", Version = "v1",
                    Description = "Ovaj API odnosi se na korisnike sistema koji se implementira, i pruza osnovne CRUD operacije.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Sanja Tica",
                        Email = "sanjatica2000@gmail.com",
                        Url = new Uri(Configuration["Swagger:Github"])
                    }
                
                });
                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                c.IncludeXmlComments(xmlCommentsPath);
            });
            services.AddDbContext<KorisnikContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Korisnik_Mikroservis v1"));
            }

            app.UseHttpsRedirection();
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

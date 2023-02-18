using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using KupacMicroservice.DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using KupacMicroservice.Data.Interfaces;
using KupacMicroservice.Data;

namespace KupacMicroservice
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
                //services.AddSingleton<IAuthRepository>(new AuthRepository(secret));
                services.AddScoped<IKupacRepository, KupacRepository>();
                services.AddScoped<IFizickoLiceRepository, FizickoLiceRepository>();
                services.AddScoped<IPravnoLiceRepository, PravnoLiceRepository>();
                services.AddScoped<ILiciterRepository, LiciterRepository>();
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
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Kupac API",
                        Version = "v1",
                        Description = "An API to perform kupac operations",
                        Contact = new OpenApiContact
                        {
                            Name = "Sanja Kijac",
                            Email = "skijac@gmail.com",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")



                        },
                        License = new OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri("http://www.ftn.uns.ac.rs/"),
                        }


                    });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);

                });
                //services.AddMvc();
                //services.AddControllers();
                services.AddDbContext<KupacDbContext>();
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KupacMikroservis v1"));
                }

                app.UseHttpsRedirection();
                //app.UseStaticFiles();
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

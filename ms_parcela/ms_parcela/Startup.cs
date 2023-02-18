using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ms_parcela.Data;
using ms_parcela.Models.ExternalServices;
using ms_parcela.ServiceCalls;
using System.Text.Json.Serialization;
using ms_parcela.Database;
using System.Reflection;

namespace ms_parcela
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
            
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<IKatastarskaOpstinaRepository, KatastarskaOpstinaRepository>();
            services.AddScoped<IZonaRepository, ZonaRepository>();
            services.AddScoped<IDeoParceleRepository, DeoParceleRepository>();
            services.AddScoped<IParcelaRepository, ParcelaRepository>();
            services.AddScoped<IServiceCall<NadmetanjeDto>, ServiceCall<NadmetanjeDto>>();

            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            

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
                    Title = "Parcela API",
                    Version = "v1",
                    Description = "API omogućava da korisnici sistema pregledaju, dodaju, menjanju i brišu podatke o katastarskim opštinama koje ulaze u proces licitacije, zonama u kojima su katastarske opštine, parcelama u okviru opština i delovima od kojih su te parcele sastavljene.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Sandra Lazarević",
                        Email = "sandra.lazarevic123@gmail.com",
                        Url = new Uri(Configuration["Swagger:Github"])
                    }

                });
                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                c.IncludeXmlComments(xmlCommentsPath);
            });
           
            
            //services.AddControllers();
            services.AddDbContext<AuctionLandAPIContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parcela_Mikroservis v1"));
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
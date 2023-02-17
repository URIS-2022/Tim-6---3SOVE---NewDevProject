using KupacMicroservice.Data;
using KupacMicroservice.Data.Interfaces;
using KupacMicroservice.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.UserModel.Charts;
using System.Reflection;
using System.Security.Policy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IKupacRepository, KupacRepository>();
builder.Services.AddScoped<IFizickoLiceRepository, FizickoLiceRepository>();
builder.Services.AddScoped<IPravnoLiceRepository, PravnoLiceRepository>();
builder.Services.AddScoped<ILiciterRepository, LiciterRepository>();
builder.Services.AddDbContext<KupacDbContext>();
builder.Services.AddSwaggerGen(c=>
{
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

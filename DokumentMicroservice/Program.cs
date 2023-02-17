using DokumentMicroservice.DataContext;
using Microsoft.EntityFrameworkCore;
using DokumentMicroservice.Data.Interfaces;
using DokumentMicroservice.Data;
using AutoMapper;
using System.Reflection.Metadata;
using Microsoft.OpenApi.Models;
using System.Reflection;

var modelbuilder = WebApplication.CreateBuilder(args);

// Add services to the container.

modelbuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
modelbuilder.Services.AddEndpointsApiExplorer();
modelbuilder.Services.AddSwaggerGen();
modelbuilder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
modelbuilder.Services.AddScoped<IDokumentRepository, DokumentRepository>();
modelbuilder.Services.AddScoped<IOglasRepository, OglasRepository>();
modelbuilder.Services.AddScoped<IPredlogPlanaProjektaRepository, PredlogPlanaProjektaRepository>();
modelbuilder.Services.AddScoped<IResenjeStrucnaKomisijaRepository, ResenjeStrucnaKomisijaRepository>();
modelbuilder.Services.AddDbContext<DokumentDbContext>();
modelbuilder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Dokument API",
        Version = "v1",
        Description = "An API to perform dokument operations",
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

var app = modelbuilder.Build();

/* modelbuilder.Services.AddDbContext<DokumentDbContext>(options =>
{ 
    options.UseSqlServer(modelbuilder.Configuration.GetConnectionString("DocumentDb"));
}); */

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(); 

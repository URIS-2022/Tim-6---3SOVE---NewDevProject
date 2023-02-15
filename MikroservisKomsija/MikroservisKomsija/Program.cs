global using MikroservisKomsija.Models;
global using MikroservisKomsija.Services;
global using MikroservisKomsija.Data;
using MikroservisKomsija.Services.ClanService;
using MikroservisKomsija.Services.KomisijaSerive;
using MikroservisKomsija.Services.KomisijaClanService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IClan, ClanRepository>();
builder.Services.AddScoped<IKomisija, KomisijaRepository>();
builder.Services.AddScoped<IKomisijaClan, KomisijaClanRepository>();
builder.Services.AddDbContext<DataContext>();

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

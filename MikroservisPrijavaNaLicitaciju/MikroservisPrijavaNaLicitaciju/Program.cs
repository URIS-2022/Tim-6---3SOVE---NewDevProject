global using MikroservisPrijavaNaLicitaciju.Model;
global using MikroservisPrijavaNaLicitaciju.Services;
global using MikroservisPrijavaNaLicitaciju.Data;
 using MikroservisPrijavaNaLicitaciju.Services.PrijavaNaLicitacijuService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPrijavaNaLicitaciju, PrijavaNaLicitacijuRepository>();
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

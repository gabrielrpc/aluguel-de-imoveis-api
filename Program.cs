using aluguel_de_imoveis.Infraestructure.DataAccess;
using aluguel_de_imoveis.Services.Interfaces;
using aluguel_de_imoveis.Services;
using Microsoft.EntityFrameworkCore;
using aluguel_de_imoveis.Repository.Interfaces;
using aluguel_de_imoveis.Repository;
using aluguel_de_imoveis.Exceptions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CONNECTION_STRING");

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
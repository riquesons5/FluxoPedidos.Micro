using FluxoPedidos.Micro.Api.Configuracoes;
using FluxoPedidos.Micro.Rabbit.Configuracoes;
using FluxoPedidos.Micro.Repository.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitConfig>(builder.Configuration.GetSection("RabbitConfig"));

builder.Services.ResolverDependencias();

builder.Services.AddDbContext<ContextoBanco>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAny");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContextoBanco>();
    db.Database.Migrate();
}

app.Run();

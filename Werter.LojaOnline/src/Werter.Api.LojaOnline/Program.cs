using System.Text.Json.Serialization;
using Werter.Api.LojaOnline.Configuracoes;
using Werter.Api.LojaOnline.Utils;

var builder = WebApplication.CreateBuilder(args);



builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AdicionarInjecaoDeDependencia(builder.Configuration);
builder.Services.AdicionarConfiguracaoDoExceptionLess(builder.Configuration);
builder.Services.AdicionarConfiguracoesDoSwagger();

var app = builder.Build();



// Pipeline
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

//Swagger
app.UseAsConfiguracoesDoSwagger();
app.UsarExceptionLess();

app.MapControllers();

app.Run();

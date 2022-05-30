using Werter.Api.LojaOnline.Configuracoes;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

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

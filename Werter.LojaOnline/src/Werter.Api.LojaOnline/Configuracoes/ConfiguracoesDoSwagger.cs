using Microsoft.OpenApi.Models;

using Werter.Api.LojaOnline.Utils;

namespace Werter.Api.LojaOnline.Configuracoes
{
    public static class ConfiguracoesDoSwagger
    {
        private const string URL_OPEN_SOURCE = "https://opensource.org/license";

        public static void AdicionarConfiguracoesDoSwagger(this IServiceCollection services)
        {
            _ = services.AddSwaggerGen(c =>
            {

                c.SchemaFilter<SwaggerSkipPropertyFilter>();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Werter - Loja Online API",
                    Description = "Aplicação de exemplo para estudos de APIs e CRUD",
                    Contact = new OpenApiContact { Name = "Werter Bonfim", Email = "werter@hotmail.com.br" },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri(URL_OPEN_SOURCE) }
                });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public static void UseAsConfiguracoesDoSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
        }
    }
}

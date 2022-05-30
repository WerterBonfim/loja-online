using Microsoft.OpenApi.Models;

namespace Werter.Api.LojaOnline.Configuracoes
{
    public static class ConfiguracoesDoSwagger
    {
        public static void AdicionarConfiguracoesDoSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Werter - Loja Online API",
                    Description = "Aplicação de exemplo para estudos de APIs e CRUD",
                    Contact = new OpenApiContact { Name = "Werter Bonfim", Email = "werter@hotmail.com.br" },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/license") }
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

using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Werter.Api.LojaOnline.Modelos;
using Werter.Api.LojaOnline.Utils;

namespace Werter.Api.LojaOnline.Controllers
{

    [Route("[controller]")]
    public class ProdutoController : BaseController
    {
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(ILogger<ProdutoController> logger)
        {
            _logger = logger;
        }




        /// <summary>
        /// Adiciona um novo produto no catalogo
        /// </summary>
        /// <param name="produto">Dados do produto a ser inserido</param>
        /// <returns></returns>
        [SwaggerResponse(200, Type = typeof(RespostaPadrao))]
        [SwaggerResponse(400, Type = typeof(List<ValidationProblemDetails>))]
        [SwaggerResponse(404, Type = typeof(List<ValidationProblemDetails>))]
        [HttpPost]
        public IActionResult NovoProduto([FromBody] Produto produto)
        {
            try
            {
                _logger.LogInformation("Novo produto adicionado com sucesso");
                return Ok(new RespostaPadrao(produto, "Produto inserido com sucesso"));
            }
            catch (Exception exception)
            {
                return RespostaPersonalizada(exception);
            }

        }

        [HttpPatch]
        public IActionResult Atualizar()
        {
            _logger.LogError("Saia da minha propriedade");

            return RespostaPersonalizada(new Exception("Erro na mão"), "Ocorreu um erro ao tentar atualizar um produto");

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Werter.Api.LojaOnline.Dados;
using Werter.Api.LojaOnline.Modelos;
using Werter.Api.LojaOnline.Negocio.Exceptions;
using Werter.Api.LojaOnline.Negocio.Requisitos;
using Werter.Api.LojaOnline.Negocio.Servicos.Produtos;
using Werter.Api.LojaOnline.Utils;

namespace Werter.Api.LojaOnline.Controllers
{

    [Route("[controller]")]
    public class ProdutoController : BaseController
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoRepositorio _repositorio;
        private readonly IServicoProdutos _servico;

        public ProdutoController(
            ILogger<ProdutoController> logger,
            IServicoProdutos servico,
            IProdutoRepositorio repositorio)
        {
            _logger = logger;
            _servico = servico;
            _repositorio = repositorio;
        }

        /// <summary>
        /// Adiciona um novo produto no catalogo
        /// </summary>
        /// <param name="produto">Dados do produto a ser inserido</param>
        /// <returns></returns>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(List<ValidationProblemDetails>))]
        [SwaggerResponse(404, Type = typeof(List<ValidationProblemDetails>))]
        [HttpPost]
        public async Task<IActionResult> NovoProduto([FromBody] RequisitosParaCadastrarProduto requisitos, CancellationToken cancellation)
        {
            try
            {
                var teste = ModelState.IsValid;
                _logger.LogInformation("Novo produto adicionado com sucesso");

                var resultado = await _servico.LidarCom(requisitos, cancellation);
                return RespostaPersonalizada(resultado);

            }
            catch (Exception exception)
            {
                return RespostaPersonalizada(exception, "Ocorreu um erro ao tentar inserir um novo produto");
            }

        }

        /// <summary>
        /// Atualiza um produto
        /// </summary>
        /// <param name="requisitos">Dados do produto a ser atualizado</param>
        /// <param name="cancellation">Cancelamento da requisição</param>
        /// <returns></returns>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(List<ValidationProblemDetails>))]
        [SwaggerResponse(404, Type = typeof(List<ValidationProblemDetails>))]
        [HttpPatch]
        public async Task<IActionResult> Atualizar([FromBody] RequisitosParaAlterarProduto requisitos, CancellationToken cancellation)
        {
            try
            {
                _logger.LogError("Alterando um produto");

                var resultado = await _servico.LidarCom(requisitos, cancellation);

                return RespostaPersonalizada(resultado);
            }
            catch (Exception exception)
            {
                return RespostaPersonalizada(exception, "Ocorreu um erro ao tentar atualizar um novo produto");
            }

        }

        /// <summary>
        /// Lista os produtos cadastrados
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(IList<Produto>))]
        [SwaggerResponse(400, Type = typeof(List<ValidationProblemDetails>))]
        [SwaggerResponse(404, Type = typeof(List<ValidationProblemDetails>))]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {

            _logger.LogError("Listando os produtos");

            var produtos = await _repositorio.ListarAsync(cancellationToken);

            return RespostaPersonalizada(produtos);

        }


        [HttpDelete]
        [SwaggerResponse(204)]
        [SwaggerResponse(400, Type = typeof(List<ValidationProblemDetails>))]
        [SwaggerResponse(404, Type = typeof(List<ValidationProblemDetails>))]
        public async Task<IActionResult> Deletar([FromQuery] RequisitosParaDeletarProduto requisitos, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogError($"Deletando o produto");

                var resultado = await _servico.LidarCom(requisitos, cancellationToken);

                return RespostaPersonalizada(resultado);
            }
            catch (Exception exception)
            {
                return RespostaPersonalizada(exception, "Ocorreu um erro ao tentar deletar um produto");
            }
        }
    }
}

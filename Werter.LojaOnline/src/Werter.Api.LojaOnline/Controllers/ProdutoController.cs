using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Werter.Api.LojaOnline.Dados;
using Werter.Api.LojaOnline.Negocio.Requisitos;
using Werter.Api.LojaOnline.Negocio.Servicos.Produtos;
using Werter.Api.LojaOnline.Utils;
using Werter.LojaOnline.Dominio.Modelos;

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
        public async Task<IActionResult> NovoProduto([FromBody] RequisitosParaCadastrarProduto requisitos,
            CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Tentando adicionar um novo produto");

                if (!requisitos.EstaValido())
                    return RespostaPersonalizada();

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
        [HttpPatch]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(List<ValidationProblemDetails>))]
        [SwaggerResponse(404, Type = typeof(List<ValidationProblemDetails>))]
        public async Task<IActionResult> Atualizar([FromBody] RequisitosParaAlterarProduto requisitos,
            CancellationToken cancellation)
        {
            _logger.LogInformation("Alterando um produto");

            var resultado = await _servico.LidarCom(requisitos, cancellation);

            return RespostaPersonalizada(resultado);
        }

        /// <summary>
        /// Lista os produtos cadastrados
        /// </summary>
        /// <param name="pagina">Indice da pagina atual</param>
        /// <param name="quantidadePorPagina">Quantidade de registros a ser retornado</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(IList<Produto>))]
        [SwaggerResponse(400, Type = typeof(List<ValidationProblemDetails>))]
        [SwaggerResponse(404, Type = typeof(List<ValidationProblemDetails>))]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken, [FromQuery] int pagina = 1,
            [FromQuery] int quantidadePorPagina = 10)
        {
            _logger.LogError("Listando os produtos");

            var skip = quantidadePorPagina * (pagina - 1);
            var produtos = await _repositorio.ListarAsync(
                cancellationToken,
                skip: skip, take: quantidadePorPagina
            );

            return RespostaPersonalizada(produtos);
        }

        /// <summary>
        /// Obtem o produto por id
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Produto</returns>
        [HttpGet("{id:guid}")]
        [SwaggerResponse(200, Type = typeof(Produto))]
        [SwaggerResponse(400, Type = typeof(List<ValidationProblemDetails>))]
        [SwaggerResponse(404, Type = typeof(List<ValidationProblemDetails>))]
        public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtendo produto: {Id} por id", id);
            var produto = await _repositorio.BuscarPorIdAsync(id, cancellationToken);
            return RespostaPersonalizada(produto);
        }


        [HttpDelete]
        [SwaggerResponse(204)]
        [SwaggerResponse(400, Type = typeof(List<ValidationProblemDetails>))]
        [SwaggerResponse(404, Type = typeof(List<ValidationProblemDetails>))]
        public async Task<IActionResult> Deletar([FromQuery] RequisitosParaDeletarProduto requisitos,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deletando o produto");
            var resultado = await _servico.LidarCom(requisitos, cancellationToken);
            return RespostaPersonalizada(resultado);
        }
    }
}
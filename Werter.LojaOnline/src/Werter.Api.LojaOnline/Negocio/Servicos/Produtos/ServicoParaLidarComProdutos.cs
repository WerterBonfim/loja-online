using Mapster;
using FluentValidation.Results;

using Werter.Api.LojaOnline.Dados;
using Werter.Api.LojaOnline.Negocio.Requisitos;

using System.Diagnostics;
using Werter.Api.LojaOnline.Utils;
using Werter.LojaOnline.Dominio.Modelos;

namespace Werter.Api.LojaOnline.Negocio.Servicos.Produtos
{
    public sealed class ServicoParaLidarComProdutos : IServicoProdutos
    {
        private readonly IProdutoRepositorio _repositorio;

        public ServicoParaLidarComProdutos(IProdutoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ValidationResult?> LidarCom(RequisitosParaCadastrarProduto requisitos, CancellationToken cancellationToken)
        {
            if (!requisitos.EstaValido())
                return requisitos.ResultadoValidacao;


            var produto = requisitos.Adapt<Produto>();

            _repositorio.Adicionar(produto);
            await _repositorio.SalvarAsync(cancellationToken);


            return requisitos.ResultadoValidacao;
        }

        public async Task<ValidationResult?> LidarCom(RequisitosParaDeletarProduto requisitos, CancellationToken cancellationToken)
        {
            if (!requisitos.EstaValido())
                return requisitos.ResultadoValidacao;


            await _repositorio.Remover(requisitos.Id, cancellationToken);
            await _repositorio.SalvarAsync(cancellationToken);


            return requisitos.ResultadoValidacao;
        }

        public async Task<ValidationResult?> LidarCom(RequisitosParaAlterarProduto requisitos, CancellationToken cancellationToken)
        {            

            if (!requisitos.EstaValido())
                return requisitos.ResultadoValidacao;

            var produtoAntigo = await _repositorio.BuscarPorIdAsync(requisitos.Id, cancellationToken);
            if (produtoAntigo == null)
                return NotificarQueProdutoNaoExiste();

            produtoAntigo.Atualizar<Produto>(requisitos);            

            _repositorio.Atualizar(produtoAntigo);

            await _repositorio.SalvarAsync(cancellationToken);


            return requisitos.ResultadoValidacao;
        }

        private static ValidationResult NotificarQueProdutoNaoExiste() =>
            new(new[] { new ValidationFailure("Id", "Produto não existe") });


    }
}

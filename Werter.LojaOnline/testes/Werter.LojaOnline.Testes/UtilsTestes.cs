using Werter.Api.LojaOnline.Modelos;
using Werter.Api.LojaOnline.Negocio.Requisitos;
using Werter.Api.LojaOnline.Utils;

namespace Werter.LojaOnline.Testes
{
    public class UtilsTestes
    {
        [Fact(DisplayName = "Deve atualizar as propriedade de uma entidade baseado na classe" +
            "de requisitos")]
        [Trait("Utils", "Propriedades")]
        public void DeveAtualizarEntidadeBaseadoNosRequisitos()
        {
            // Act
            var produto = new Produto("Valor velho", "Descricao Velha", 100, 0);
            var requisitos = new RequisitosParaAlterarProduto
            {
                Nome = "Valor novo",
                Descricao = "Descricao Nova"
            };

            // Arrange

            produto.Atualizar<Produto>(requisitos);

            // Act

            Assert.Equal(produto.Nome, requisitos.Nome);
            Assert.Equal(produto.Descricao, requisitos.Descricao);
            Assert.Equal(100, produto.Valor);
        }


       


    }
}
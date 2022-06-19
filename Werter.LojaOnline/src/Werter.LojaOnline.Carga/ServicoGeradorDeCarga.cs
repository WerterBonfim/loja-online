using System.Data.SqlClient;
using Bogus;
using DapperLike;
using Werter.LojaOnline.Dominio.Modelos;

namespace Werter.LojaOnline.Carga;

public sealed class ServicoGeradorDeCarga
    {
        private readonly Random _random = new(8675309);
        private const int QtdMaximaItens = 10_000;

        public void GerarCargaFake(ParametrosCarga parametros)
        {
            using var conexao = new SqlConnection(parametros.StringDeConexao);

            while (parametros.QuantidadeDeRegistros > 0)
            {
                Console.WriteLine($"Faltam {parametros.QuantidadeDeRegistros} registros...");
                if (parametros.QuantidadeDeRegistros > QtdMaximaItens)
                {
                    var cargaGrande = GerarLista(parametros.NomeDaTabela, QtdMaximaItens);
                    InserirMassa(cargaGrande, parametros.NomeDaTabela);
                    parametros.QuantidadeDeRegistros -= QtdMaximaItens;
                    continue;
                }

                var cargaLeve = GerarLista(parametros.NomeDaTabela, parametros.QuantidadeDeRegistros);
                InserirMassa(cargaLeve, parametros.NomeDaTabela);
                break;
            }


            void InserirMassa(IEnumerable<Produto> produtos, string nomeDaTabela) =>
                 conexao.BulkInsert(produtos, nomeDaTabela);

        }

        private List<Produto> GerarLista(string nomeDaTabela, int quantidadeDeRegistros)
        {
            var fake = new Faker<Produto>("pt_BR")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.DataHoraCadastro, x => BetweenDates(x))
                .RuleFor(x => x.Nome, x => x.Commerce.ProductName())
                .RuleFor(x => x.Descricao, x => x.Commerce.ProductDescription())
                .RuleFor(x => x.Valor, x => Convert.ToDecimal( x.Commerce.Price(50, 1000, 2)))
                .RuleFor(x => x.QuantidadeEmEstoque, x => _random.Next(0, 100))
                
                ;
            

            DateTime BetweenDates(Faker faker) =>
                faker.Date
                    .Between(
                        new DateTime(2000, 1, 1), 
                        DateTime.Now);
            
            return fake.Generate(quantidadeDeRegistros);
        }


    }
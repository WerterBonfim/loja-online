using Werter.LojaOnline.Carga;

Console.WriteLine("Gerando carga de produtos");

new ServicoGeradorDeCarga().GerarCargaFake(new ParametrosCarga
{
    NomeDaTabela = "Produtos",
    StringDeConexao = "Server=localhost,1433;Database=LojaOnline;User Id=sa;Password=!123Senha;MultipleActiveResultSets=true;Connection Timeout=5",
    QuantidadeDeRegistros = 500_000
});


Console.WriteLine("Terminou 😄");
namespace Werter.Api.LojaOnline.Utils;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class MensagemDeLogAttribute : Attribute
{
    public string Informacao { get; init; }
    public string Erro { get; init; }

    public MensagemDeLogAttribute(string informacao, string erro)
    {
        Informacao = informacao;
        Erro = erro;
    }
}
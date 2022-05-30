namespace Werter.Api.LojaOnline.Utils
{
    public class RespostaPadrao
    {
        public object Dados { get; set; }        
        public string Mensagem { get; set; }

        public RespostaPadrao()
        {

        }

        public RespostaPadrao(object dados, string mensagem = null)
        {

        }
    }
}

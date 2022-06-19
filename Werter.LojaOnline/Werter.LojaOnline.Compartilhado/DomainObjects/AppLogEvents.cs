using Microsoft.Extensions.Logging;

namespace Werter.LojaOnline.Compartilhado.DomainObjects;

public static class AppLogEvents
{
    public static EventId Create = new(1000, "Criado");
    public static EventId Read = new(2000, "Leitura");
    public static EventId Update = new(3000, "Atualizar");
    public static EventId Delete = new(4000, "Deletar");
    public static EventId Error = new(5000, "Erro");
    
}
using System.Reflection;
using Werter.Api.LojaOnline.Modelos;
using Werter.Api.LojaOnline.Negocio.Exceptions;
using Werter.Api.LojaOnline.Negocio.Requisitos;

namespace Werter.Api.LojaOnline.Utils
{

    /// <summary>
    /// As Propriedades marcadas com esse atributos, são preservadas, seu valor 
    /// não é atualizado
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NaoDeveAtualizarAttribute : Attribute { }

    public static class AtualizadorDeObjetos
    {
        public static void Atualizar<EntidadeBase>(this EntidadeBase entidade, RequisitosBase requisitos) 
        {
            var propriedadesDoRequisito = requisitos
                .GetType()
                .GetProperties()
                .Where(PropriedadesComValor(requisitos))
                .ToDictionary(x => x.Name, y => y.GetValue(requisitos));

            if (entidade == null)
                throw new LojaOnlineException("o objeto entidade não foi definida");

            var nomeDasPropriedadesASerAtualizada = entidade
                .GetType()
                .GetProperties()
                .Where(x => propriedadesDoRequisito.ContainsKey(x.Name));

            foreach (var propriedade in nomeDasPropriedadesASerAtualizada)
            {
                var valor = propriedadesDoRequisito[propriedade.Name];
                propriedade.SetValue(entidade, valor);
            }

        }

        // 
        private static Func<PropertyInfo, bool> PropriedadesComValor(RequisitosBase requisitos)
            => item => item.GetValue(requisitos) != null &&
                       item.GetCustomAttribute<NaoDeveAtualizarAttribute>() == null;

    }
}

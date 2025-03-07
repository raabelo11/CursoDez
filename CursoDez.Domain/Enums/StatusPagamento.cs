using System.ComponentModel;

namespace CursoDez.Domain.Enums
{
    public enum StatusPagamento
    {
        [Description("Pagamento Criado")]
        PagCriado = 1,

        [Description("Pagamento Pendente")]
        PagPendente = 2,

        [Description("Pagamento Aprovado")]
        PagAprovado = 3
    }
}

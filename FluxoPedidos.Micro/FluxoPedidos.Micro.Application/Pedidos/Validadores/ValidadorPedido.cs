using FluxoPedidos.Micro.Domain.Models.Pedidos;
using FluxoPedidos.Micro.Domain.Models.Pedidos.Itens;

namespace FluxoPedidos.Micro.Application.Pedidos.Validadores
{
    public class ValidadorPedido : IValidadorPedido
    {
        public string? Validar(Pedido pedido)
        {
            if (pedido.ClienteId <= 0 && pedido.Cliente is null)
                return "O pedido deve estar associado a um cliente válido.";

            if (pedido.Total < 0)
                return "O valor total do pedido não pode ser negativo.";

            if (pedido.Itens is null || pedido.Itens.Count == 0)
                return "O pedido deve conter pelo menos um item.";

            foreach (var item in pedido.Itens)
            {
                var erroItem = ValidarItem(item);
                if (erroItem is not null)
                    return $"Erro no item: {erroItem}";
            }

            return null;
        }

        private string? ValidarItem(Item item)
        {
            if (string.IsNullOrWhiteSpace(item.Produto))
                return "O produto é obrigatório.";

            if (item.Quantidade <= 0)
                return "A quantidade deve ser maior que zero.";

            if (item.Preco <= 0)
                return "O preço deve ser maior que zero.";

            return null;
        }
    }
}

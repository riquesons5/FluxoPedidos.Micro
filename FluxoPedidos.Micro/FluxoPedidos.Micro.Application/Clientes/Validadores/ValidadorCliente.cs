using FluxoPedidos.Micro.Domain.Models.Clientes;

namespace FluxoPedidos.Micro.Application.Clientes.Validadores
{
    public class ValidadorCliente : IValidadorCliente
    {
        public string? Validar(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
                return "O campo nome é obrigatório";

            if (string.IsNullOrWhiteSpace(cliente.Documento))
                return "O campo Documento é obrigatório.";

            if (!DocumentoValido(cliente.Documento))
                return "O Documento deve conter apenas números e ter 11 (CPF) ou 14 (CNPJ) dígitos.";

            return null;
        }

        private bool DocumentoValido(string documento)
        {
            documento = documento.Trim();

            if (!documento.All(char.IsDigit))
                return false;

            return documento.Length == 11 || documento.Length == 14;
        }
    }
}

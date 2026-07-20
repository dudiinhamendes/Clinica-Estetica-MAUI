namespace SistemaClinica.Models
{
    // Representa os dados que a API retorna apos um login bem-sucedido
    public record LoginResponse(string Token, string Nome, string Email);
}
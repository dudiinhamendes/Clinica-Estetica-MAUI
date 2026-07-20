namespace SistemaClinica.Models
{
    // Representa os dados que o app envia para a API ao fazer login
    public record LoginRequest(string Email, string Senha);
}
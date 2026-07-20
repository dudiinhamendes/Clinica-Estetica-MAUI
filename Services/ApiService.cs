using System.Net.Http.Json;
using SistemaClinica.Models;

namespace SistemaClinica.Services
{
    // Esta classe e responsavel por toda a comunicacao com a API
    // Ela fica separada da tela para que qualquer pagina possa usa-la
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        // O HttpClient e recebido automaticamente pelo sistema de injecao de dependencia
        // Nao criamos um "new HttpClient()" aqui de proposito
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Envia email e senha para a API e retorna o resultado
        // Retorna null se as credenciais estiverem erradas ou se a API estiver offline
        public async Task<LoginResponse?> LoginAsync(string email, string senha)
        {
            try
            {
                var request = new LoginRequest(email, senha);

                // Faz a requisicao POST para /login com o objeto serializado em JSON
                var response = await _httpClient.PostAsJsonAsync("/login", request);

                // Se a resposta nao for 200 OK, retorna null
                if (!response.IsSuccessStatusCode)
                    return null;

                // Converte o JSON da resposta para o tipo LoginResponse
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            catch (HttpRequestException)
            {
                // Erro de rede: API offline, sem internet, endereco errado, etc.
                return null;
            }
        }
    }
}
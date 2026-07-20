using SistemaClinica.ViewModels;
using SistemaClinica.Services;

namespace SistemaClinica.Views;

public partial class LoginPage : ContentPage
{

    // Guarda a referencia ao ApiService recebido pelo construtor
    private readonly ApiService _apiService;

    // O ApiService e entregue automaticamente pelo sistema de injecao de dependencia
    // que configuramos no MauiProgram.cs
    public LoginPage(ApiService apiService)
    {
        InitializeComponent();

        _apiService = apiService;

        _viewModel = new LoginViewModel();

        Shell.SetNavBarIsVisible(this, false);
    }

    private LoginViewModel _viewModel;

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EntryEmail.Text?.Trim();
        string senha = EntrySenha.Text?.Trim();

        // 1. Valida apenas se os campos estão em branco localmente
        string erro = _viewModel.ValidarCampos(email, senha);

        if (!string.IsNullOrEmpty(erro))
        {
            LabelErro.Text = erro;
            LabelErro.IsVisible = true;
            return;
        }

        LabelErro.IsVisible = false;
        MostrarCarregando(true);

        await Task.Delay(1000); // Pequeno delay apenas para o efeito visual

        var resultado = await _apiService.LoginAsync(email, senha);

        MostrarCarregando(false);

        if (resultado is not null)
        {

            SessaoUsuario.Nome = resultado.Nome;
            SessaoUsuario.Email = resultado.Email;
            SessaoUsuario.Token = resultado.Token;

            // Login bem-sucedido: exibe o alerta
            await DisplayAlertAsync("Sucesso", $"Bem-vindo, {resultado.Nome}!\nToken: {resultado.Token}", "OK");

            if (resultado.Nome == "Administrador")
            {
                await Shell.Current.GoToAsync("//HomeAdminPage");
            }
            else
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
        }
        else
        {
            // Se a API retornar null (dados incorretos ou API desligada), exibe o erro
            MostrarErro("E-mail ou senha incorretos.\nVerifique se a API está rodando.");
        }
    }

    private async void OnEsqueceuSenhaClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Recuperar Senha", "Um link de recuperação será enviado para o seu e-mail.", "OK");
    }

    private async void OnCadastreClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CadastroPage");
    }

    private void MostrarErro(string mensagem)
    {
        LabelErro.Text = mensagem;
        LabelErro.IsVisible = true;
    }

    private void EsconderErro()
    {
        LabelErro.IsVisible = false;
        LabelErro.Text = string.Empty;
    }

    private void MostrarCarregando(bool ativo)
    {
        Carregando.IsVisible = ativo;
        Carregando.IsRunning = ativo;
        BotaoLogin.IsEnabled = !ativo;
    }
}

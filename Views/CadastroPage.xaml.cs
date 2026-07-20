using SistemaClinica.ViewModels;
using SistemaClinica.Services;

namespace SistemaClinica.Views;

public partial class CadastroPage : ContentPage
{
    private CadastroViewModel _viewModel;
    public CadastroPage()
    {
        InitializeComponent();

        _viewModel = new CadastroViewModel();

        Shell.SetNavBarIsVisible(this, false);
    }

    private async void OnCadastroClicked(object sender, EventArgs e)
    {
        string nomeCompleto = EntryNomeCompleto.Text?.Trim();
        string cpf = EntryCPF.Text?.Trim();
        string telefone = EntryTelefone.Text?.Trim();
        string email = EntryEmail.Text?.Trim();
        string senha = EntrySenha.Text?.Trim();
        string ConfirmarSenha = EntryConfirmarSenha.Text?.Trim();

        string erro = _viewModel.ValidarCampos(nomeCompleto, cpf, telefone, email, senha, ConfirmarSenha);

        if (!string.IsNullOrEmpty(erro))
        {
            LabelErro.Text = erro;
            LabelErro.IsVisible = true;
            return;
        }

        bool sucesso = _viewModel.CadastrarUsuario(nomeCompleto, cpf, telefone, email, senha);

        if (sucesso)
        {
            await DisplayAlert("Sucesso", "Usuário Cadastrado com sucesso!", "OK");

            await Shell.Current.GoToAsync("//HomePage");
        }

    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
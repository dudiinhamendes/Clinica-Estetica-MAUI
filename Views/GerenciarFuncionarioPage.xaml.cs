using SistemaClinica.ViewModels;
using SistemaClinica.Services;
using SistemaClinica.Models;

namespace SistemaClinica.Views;

public partial class GerenciarFuncionarioPage : ContentPage
{
    private GerenciarFuncionarioViewModel _viewModel;
    private Funcionario? funcionarioEmEdicao;
    public GerenciarFuncionarioPage()
    {
        InitializeComponent();

        _viewModel = new GerenciarFuncionarioViewModel();

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.CarregarServicos();
        await _viewModel.CarregarFuncionarios();
    }

    private async void OnVoltarFuncionarioClicked(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//HomeAdminPage");
    }

    private async void OnEditarFuncionarioClicked(object sender, TappedEventArgs e)
    {
        var border = sender as Border;

        if (border?.BindingContext is Funcionario funcionario)
        {
            funcionarioEmEdicao = funcionario;

            EntryNomeFuncionario.Text = funcionario.Nome_funcionario;

            foreach (Servico servico in _viewModel.Servicos)
            {
                if (servico.Nome_servico == funcionario.Nome_servico)
                {
                    PickerEspecialidade.SelectedItem = servico;
                    break;
                }
            }

            PickerDisponibilidade.SelectedItem = funcionario.Disponivel ? "Disponível" : "Indisponível";

            ModalNovoFuncionario.IsVisible = true;
        }
    }

    private async void OnCancelarFuncionarioClicked(object sender, TappedEventArgs e)
    {
        var border = sender as Border;

        if (border?.BindingContext is Funcionario funcionario)
        {
            bool resposta = await DisplayAlert("Remover Funcionário", $"Deseja realmente remover {funcionario.Nome_funcionario}?", "Sim", "Não");

            if (resposta)
            {
                await _viewModel.ExcluirFuncionario(funcionario);

                await DisplayAlert("Sucesso", "Funcionário removido com sucesso!", "OK");
            }
        }
    }

    private void OnNovoFuncionarioClicked(object sender, TappedEventArgs e)
    {

        funcionarioEmEdicao = null;

        EntryNomeFuncionario.Text = string.Empty;

        PickerEspecialidade.SelectedIndex = -1;

        PickerDisponibilidade.SelectedIndex = -1;

        LabelErro.IsVisible = false;
        LabelErro.Text = string.Empty;

        ModalNovoFuncionario.IsVisible = true;
    }

    private void OnCancelarModalFuncionarioClicked(object sender, EventArgs e)
    {
        ModalNovoFuncionario.IsVisible = false;
    }
    private async void OnSalvarFuncionarioClicked(object sender, EventArgs e)
    {
        string NomeFuncionario = EntryNomeFuncionario.Text?.Trim();

        Servico servicoSelecionado = PickerEspecialidade.SelectedItem as Servico;
        string NomeServico = servicoSelecionado?.Nome_servico;

        string DisponibilidadeTexto = PickerDisponibilidade.SelectedItem?.ToString();

        string mensagemErro = _viewModel.ValidarCampos(NomeFuncionario, NomeServico, DisponibilidadeTexto);

        if (!string.IsNullOrEmpty(mensagemErro))
        {
            LabelErro.Text = mensagemErro;
            LabelErro.IsVisible = true;
            return; 
        }

        bool disponibilidadeBool = DisponibilidadeTexto == "Disponível";

        if (funcionarioEmEdicao == null)
        {
            bool sucesso = await _viewModel.CadastrarFuncionario(NomeFuncionario, NomeServico, disponibilidadeBool);

            if (sucesso)
            {
                await DisplayAlert("Sucesso", "Funcionário cadastrado com sucesso!", "OK");
            }
        }
        else
        {
            funcionarioEmEdicao.Nome_funcionario = NomeFuncionario;
            funcionarioEmEdicao.Nome_servico = NomeServico;
            funcionarioEmEdicao.Disponivel = disponibilidadeBool;

            await _viewModel.EditarFuncionario(funcionarioEmEdicao);

            await DisplayAlert("Sucesso", "Funcionário atualizado com sucesso!", "OK");

            funcionarioEmEdicao = null;
        }

        await _viewModel.CarregarFuncionarios();

        EntryNomeFuncionario.Text = string.Empty;
        PickerEspecialidade.SelectedIndex = -1;
        PickerDisponibilidade.SelectedIndex = -1;

        LabelErro.IsVisible = false;
        ModalNovoFuncionario.IsVisible = false;
    }
}
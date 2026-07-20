using SistemaClinica.ViewModels;
using SistemaClinica.Services;
using SistemaClinica.Models;

namespace SistemaClinica.Views;
public partial class GerenciarServicoPage : ContentPage
{
    private readonly GerenciarServicoViewModel _viewModel;
    private Servico? servicoEmEdicao;
    public GerenciarServicoPage()
    {

        InitializeComponent();

        _viewModel = new GerenciarServicoViewModel();

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.CarregarServicos();
    }

    private async void OnVoltarServicoClicked(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//HomeAdminPage");
    }

    private async void OnEditarServicoClicked(object sender, TappedEventArgs e)
    {
        var border = sender as Border;

        if (border?.BindingContext is Servico servico)
        {
            servicoEmEdicao = servico;

            EntryNomeServico.Text = servico.Nome_servico;
            EntryValorServico.Text = servico.Preco.ToString();
            EntryDuracaoServico.Text = servico.Duracao.ToString();

            ModalNovoServico.IsVisible = true;
        }
    }

    private async void OnCancelarServicoClicked(object sender, TappedEventArgs e)
    {
        bool resposta = await DisplayAlert("Cancelar Serviço", "Deseja realmente cancelar este serviço?", "Sim", "Não");

        if (!resposta)
            return;

        var border = sender as Border;

        if (border?.BindingContext is Servico servico)
        {
            await _viewModel.ExcluirServico(servico.Id_servico);

            await DisplayAlert("Sucesso", "Serviço excluído com sucesso!", "OK");

        }
    }

    private void OnNovoServicoClicked(object sender, TappedEventArgs e)
    {
        servicoEmEdicao = null;

        EntryNomeServico.Text = string.Empty;
        EntryValorServico.Text = string.Empty;
        EntryDuracaoServico.Text = string.Empty;

        ModalNovoServico.IsVisible = true;
    }

    private void OnCancelarModalClicked(object sender, EventArgs e)
    {
        ModalNovoServico.IsVisible = false;
    }

    private async void OnSalvarServicoClicked(object sender, EventArgs e)
    {
        string NomeServico = EntryNomeServico.Text?.Trim();
        string Preco = EntryValorServico.Text?.Trim();
        string Duracao = EntryDuracaoServico.Text?.Trim();

        string erro = _viewModel.ValidarCampos(NomeServico, Preco, Duracao);

        if (!string.IsNullOrEmpty(erro))
        {
            LabelErro.Text = erro;
            LabelErro.IsVisible = true;
            return;
        }

        LabelErro.IsVisible = false;

        decimal preco = decimal.Parse(Preco);
     
        int duracao = int.Parse(Duracao);

        if (servicoEmEdicao == null)
        {
            bool sucesso = await _viewModel.CadastrarServico(NomeServico, preco, duracao);

            if (sucesso)
            {
                await DisplayAlert("Sucesso", "Serviço cadastrado com sucesso!", "OK");
            }
        }
        else
        {
            servicoEmEdicao.Nome_servico = NomeServico;
            servicoEmEdicao.Preco = preco;
            servicoEmEdicao.Duracao = duracao;

            await _viewModel.EditarServico(servicoEmEdicao);

            await DisplayAlert("Sucesso", "Serviço atualizado com sucesso!", "OK");
        }

        servicoEmEdicao = null;

        EntryNomeServico.Text = string.Empty;
        EntryValorServico.Text = string.Empty;
        EntryDuracaoServico.Text = string.Empty;

        ModalNovoServico.IsVisible = false;
    }
}



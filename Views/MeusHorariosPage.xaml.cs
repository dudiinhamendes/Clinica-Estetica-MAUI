using SistemaClinica.ViewModels;
using SistemaClinica.Services;
using SistemaClinica.Models;

namespace SistemaClinica.Views;

public partial class MeusHorariosPage : ContentPage
{
    private readonly MeusHorariosViewModel _viewModel;
    public MeusHorariosPage()
    {
        InitializeComponent();

        _viewModel = new MeusHorariosViewModel();

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.CarregarMeusAgendamentos();
    }

    private async void OnVoltarClicked(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }

    private async void OnEditarClicked(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Teste", "Tela em andamento!", "Ok");
    }

    private async void OnCancelarClicked(object sender, TappedEventArgs e)
    {
        var border = sender as Border;

        if (border?.BindingContext is not Agendamento agendamento)
            return;

        bool resposta = await DisplayAlert("Cancelar Agendamento", "Deseja realmente cancelar este agendamento?", "Sim", "Não");

        if (!resposta)
            return;

        await _viewModel.ExcluirAgendamento(agendamento);

        await DisplayAlert("Sucesso", "Agendamento cancelado.", "OK");
    }
}
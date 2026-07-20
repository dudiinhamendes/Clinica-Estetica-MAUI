using SistemaClinica.ViewModels;
using SistemaClinica.Services;
using SistemaClinica.Models;

namespace SistemaClinica.Views;

public partial class ServicosPage : ContentPage
{
	private readonly ServicosViewModel _viewModel;
	public ServicosPage()
	{
		InitializeComponent();

		_viewModel = new ServicosViewModel();

		BindingContext = _viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		await _viewModel.CarregarServicos();
	}

	private void EntryPesquisarServico_TextChanged(object sender, TextChangedEventArgs e)
	{
		_viewModel.PesquisarServico(e.NewTextValue);
	}
	private async void OnVoltarClicked(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("//HomePage");
	}
}
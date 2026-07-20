namespace SistemaClinica.Views;

public partial class GerenciarUsuarioPage : ContentPage
{
	public GerenciarUsuarioPage()
	{
		InitializeComponent();
	}

	private async void OnVoltarUsuarioClicked(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("//HomeAdminPage");
	}

	private async void OnEditarUsuarioClicked(object sender, TappedEventArgs e)
	{
		await DisplayAlert("Teste", "Tela em andamento!", "Ok");
	}

	private async void OnCancelarUsuarioClicked(object sender, TappedEventArgs e)
	{
		await DisplayAlert("Teste", "Tela em andamento!", "Ok");

	}
}
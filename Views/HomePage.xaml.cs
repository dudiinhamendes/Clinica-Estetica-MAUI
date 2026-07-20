namespace SistemaClinica.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnMeusHorariosClicked(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//MeusHorariosPage");
    }

    private async void OnServicosClicked(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//ServicosPage");
    }

    private async void OnAgendamentosClicked(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//AgendamentoPage");
    }

}

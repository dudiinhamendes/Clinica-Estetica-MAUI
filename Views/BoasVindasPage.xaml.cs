using SistemaClinica.ViewModels;

namespace SistemaClinica.Views;
public partial class BoasVindasPage : ContentPage
{
    public BoasVindasPage()
    {
        InitializeComponent();

        IniciarTela();
    }

    private async void IniciarTela()
    {
        var vm = new BoasVindasViewModel();

        await vm.IrParaLogin();
    }
}
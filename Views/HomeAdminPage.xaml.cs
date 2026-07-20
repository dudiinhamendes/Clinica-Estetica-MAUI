using SistemaClinica.Services;
using SistemaClinica.ViewModels;

namespace SistemaClinica.Views;

public partial class HomeAdminPage : ContentPage
{
        private readonly HomeAdminViewModel _viewModel;
        public HomeAdminPage()
        {
                InitializeComponent();

                _viewModel = new HomeAdminViewModel();

                BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
                base.OnAppearing();

                await _viewModel.CarregarResumo();
        }

        private async void OnGerenciarServicoClicked(object sender, TappedEventArgs e)
        {

                await Shell.Current.GoToAsync("//GerenciarServicoPage");
        }

        private async void OnGerenciarFuncionarioClicked(object sender, TappedEventArgs e)
        {

                await Shell.Current.GoToAsync("//GerenciarFuncionarioPage");
        }

        private async void OnGerenciarAgendamentoClicked(object sender, TappedEventArgs e)
        {

                await Shell.Current.GoToAsync("//GerenciarAgendamentoPage");
        }

        private async void OnGerenciarUsuarioClicked(object sender, TappedEventArgs e)
        {

                await Shell.Current.GoToAsync("//GerenciarUsuarioPage");
        }
}
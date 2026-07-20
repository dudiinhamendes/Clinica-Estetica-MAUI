using System.Linq;
using SistemaClinica.ViewModels;
using SistemaClinica.Services;
using SistemaClinica.Models;

namespace SistemaClinica.Views;

public partial class AgendamentoPage : ContentPage
{
    private readonly AgendamentoViewModel _viewModel;
    private TimeSpan _horarioSelecionado;
    private Border _horarioAnterior;
    public AgendamentoPage()
    {
        InitializeComponent();

        _viewModel = new AgendamentoViewModel();

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.CarregarServicos();
        await _viewModel.CarregarFuncionarios();
    }

    private async void OnVoltarClickedAgendamento(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }

    private async void PickerServico_SelectedIndexChanged(object sender, EventArgs e)
    {
        Servico servicoSelecionado = PickerServico.SelectedItem as Servico;

        if (servicoSelecionado == null)
            return;

        Funcionario funcionario = _viewModel.Funcionarios.FirstOrDefault(f => f.Nome_servico == servicoSelecionado.Nome_servico);

        if (funcionario != null)
        {
            PickerFuncionario.SelectedItem = funcionario;

            await AtualizarHorariosDisponiveis();
        }
    }

    private void HorarioSelecionado(object sender, TappedEventArgs e)
    {
        Border horario = (Border)sender;

        if (_horarioAnterior != null)
        {
            _horarioAnterior.BackgroundColor = Color.FromArgb("#F7EEE8");
            _horarioAnterior.Stroke = Color.FromArgb("#D8C0B2");

            Label lblAnterior = _horarioAnterior.Content as Label;

            if (lblAnterior != null)
                lblAnterior.TextColor = Color.FromArgb("#4A2419");
        }

        horario.BackgroundColor = Color.FromArgb("#4A2419");
        horario.Stroke = Color.FromArgb("#4A2419");

        Label lbl = horario.Content as Label;

        if (lbl != null)
            lbl.TextColor = Colors.White;

        _horarioAnterior = horario;

        _horarioSelecionado = TimeSpan.Parse(horario.ClassId);
    }

    private async void OnAgendarClicked(object sender, EventArgs e)
    {
        LabelErro.IsVisible = false;

        Servico servicoSelecionado = PickerServico.SelectedItem as Servico;

        Funcionario funcionarioSelecionado = PickerFuncionario.SelectedItem as Funcionario;

        if (servicoSelecionado == null)
        {
            LabelErro.Text = "Selecione um serviço";
            LabelErro.IsVisible = true;
            return;
        }

        if (funcionarioSelecionado == null)
        {
            LabelErro.Text = "Selecione um funcionário";
            LabelErro.IsVisible = true;
            return;
        }

        if (_horarioSelecionado == TimeSpan.Zero)
        {
            LabelErro.Text = "Selecione um horário";
            LabelErro.IsVisible = true;
            return;
        }

        bool sucesso = await _viewModel.CadastrarAgendamento(servicoSelecionado, funcionarioSelecionado, DateAgendamento.Date ?? DateTime.Today, _horarioSelecionado);

        if (sucesso)
        {
            await DisplayAlert("Sucesso", "Agendamento realizado com sucesso!", "OK");

            PickerServico.SelectedIndex = -1;
            PickerFuncionario.SelectedIndex = -1;
            DateAgendamento.Date = DateTime.Today;
            _horarioSelecionado = TimeSpan.Zero;

            await Task.Delay(2000);

            await Shell.Current.GoToAsync("//HomePage");
        }
        else
        {
            await DisplayAlert("Erro", "Não foi possível realizar o agendamento.", "OK");
        }
    }
    private async Task AtualizarHorariosDisponiveis()
    {
        Funcionario funcionario = PickerFuncionario.SelectedItem as Funcionario;

        if (funcionario == null)
            return;

        var agendamentos = await _viewModel.ListarAgendamentos();

        DateTime data = DateAgendamento.Date ?? DateTime.Today;

        bool ocupado0900 = agendamentos.Any(a => a.Nome_funcionario == funcionario.Nome_funcionario && a.Data_Horario.Date == data.Date && a.Data_Horario.TimeOfDay == new TimeSpan(9, 0, 0));

        bool ocupado0930 = agendamentos.Any(a => a.Nome_funcionario == funcionario.Nome_funcionario && a.Data_Horario.Date == data.Date && a.Data_Horario.TimeOfDay == new TimeSpan(9, 30, 0));

        bool ocupado1000 = agendamentos.Any(a => a.Nome_funcionario == funcionario.Nome_funcionario && a.Data_Horario.Date == data.Date && a.Data_Horario.TimeOfDay == new TimeSpan(10, 0, 0));

        AlterarEstadoHorario(Horario0900, ocupado0900);
        AlterarEstadoHorario(Horario0930, ocupado0930);
        AlterarEstadoHorario(Horario1000, ocupado1000);
    }

    private async void DateAgendamento_DateSelected(object sender, DateChangedEventArgs e)
    {
        await AtualizarHorariosDisponiveis();
    }

    private void AlterarEstadoHorario(Border horario, bool ocupado)
    {
        horario.IsEnabled = !ocupado;

        if (ocupado)
        {
            horario.BackgroundColor = Color.FromArgb("#D9D9D9");
            horario.Stroke = Color.FromArgb("#B0B0B0");

            if (horario.Content is Label lbl)
                lbl.TextColor = Colors.Gray;
        }
        else
        {
            horario.BackgroundColor = Color.FromArgb("#F7EEE8");
            horario.Stroke = Color.FromArgb("#D8C0B2");

            if (horario.Content is Label lbl)
                lbl.TextColor = Color.FromArgb("#4A2419");
        }
    }
}
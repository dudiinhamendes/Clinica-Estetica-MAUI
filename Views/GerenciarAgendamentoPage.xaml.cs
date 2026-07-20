using SistemaClinica.ViewModels;
using SistemaClinica.Services;
using SistemaClinica.Models;

namespace SistemaClinica.Views;
public partial class GerenciarAgendamentoPage : ContentPage
{
    private readonly AgendamentosAdminViewModel _viewModel;
    private Agendamento? agendamentoEmEdicao;
    public GerenciarAgendamentoPage()
    {
        InitializeComponent();

        _viewModel = new AgendamentosAdminViewModel();

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.CarregarServicos();
        await _viewModel.CarregarFuncionarios();
        await _viewModel.CarregarAgendamentos();
    }

    private async void OnVoltarAgendamentoClicked(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//HomeAdminPage");
    }

    private void OnEditarAgendamentoClicked(object sender, TappedEventArgs e)
    {
        var border = sender as Border;

        if (border?.BindingContext is Agendamento agendamento)
        {
            agendamentoEmEdicao = agendamento;

            PickerCliente.SelectedItem = agendamento.Nome_cliente;

            foreach (Funcionario funcionario in _viewModel.Funcionarios)
            {
                if (funcionario.Nome_funcionario == agendamento.Nome_funcionario)
                {
                    PickerFuncionario.SelectedItem = funcionario;
                    break;
                }
            }

            foreach (Servico servico in _viewModel.Servicos)
            {
                if (servico.Nome_servico == agendamento.Nome_servico)
                {
                    PickerServico.SelectedItem = servico;
                    break;
                }
            }

            PickerStatus.SelectedItem = agendamento.Status;
            DateAgendamento.Date = agendamento.Data_Horario.Date;
            TimeAgendamento.Time = agendamento.Data_Horario.TimeOfDay;

            ModalNovoAgendamento.IsVisible = true;
        }
    }

    private async void OnCancelarAgendamentoClicked(object sender, TappedEventArgs e)
    {
        var border = sender as Border;

        if (border?.BindingContext is not Agendamento agendamento)
            return;

        bool resposta = await DisplayAlert("Remover Agendamento", "Deseja realmente remover este agendamento?", "Sim", "Não");

        if (!resposta)
            return;

        await _viewModel.ExcluirAgendamento(agendamento);

        await DisplayAlert("Sucesso", "Agendamento removido com sucesso!", "OK");
    }

    private void OnNovoAgendamentoClicked(object sender, TappedEventArgs e)
    {
        agendamentoEmEdicao = null;

        PickerCliente.SelectedIndex = -1;
        PickerFuncionario.SelectedIndex = -1;
        PickerServico.SelectedIndex = -1;

        DateAgendamento.Date = DateTime.Today;

        TimeAgendamento.Time = new TimeSpan(9, 0, 0);

        PickerStatus.SelectedIndex = -1;

        LabelErro.IsVisible = false;
        LabelErro.Text = string.Empty;

        ModalNovoAgendamento.IsVisible = true;
    }
    private void OnCancelarModalAgendamentoClicked(object sender, EventArgs e)
    {
        ModalNovoAgendamento.IsVisible = false;
    }

    private async void OnSalvarAgendamentoClicked(object sender, EventArgs e)
    {

        string NomeCliente = PickerCliente.SelectedItem?.ToString();

        Funcionario funcionarioSelecionado = PickerFuncionario.SelectedItem as Funcionario;

        Servico servicoSelecionado = PickerServico.SelectedItem as Servico;

        string NomeFuncionario = funcionarioSelecionado?.Nome_funcionario;

        string NomeServico = servicoSelecionado?.Nome_servico;

        string Status = PickerStatus.SelectedItem?.ToString();

        DateTime DataAgendamento = DateAgendamento.Date ?? DateTime.Today;

        TimeSpan HoraAgendamento = TimeAgendamento.Time ?? TimeSpan.Zero;

        string erro = _viewModel.ValidarCampos(NomeCliente, NomeServico, NomeFuncionario, DataAgendamento, HoraAgendamento, Status);

        if (!string.IsNullOrEmpty(erro))
        {
            LabelErro.Text = erro;
            LabelErro.IsVisible = true;
            return;
        }

        LabelErro.IsVisible = false;

        if (agendamentoEmEdicao == null)
        {
            bool sucesso = await _viewModel.CadastrarAgendamento(NomeCliente, NomeServico, NomeFuncionario, DataAgendamento, HoraAgendamento, Status);

            if (sucesso)
            {
                await DisplayAlert("Sucesso","Agendamento cadastrado com sucesso!", "OK");
            }
        }
        else
        {
            agendamentoEmEdicao.Nome_cliente = NomeCliente;
            agendamentoEmEdicao.Nome_funcionario = NomeFuncionario;
            agendamentoEmEdicao.Nome_servico = NomeServico;
            agendamentoEmEdicao.Data_Horario = DataAgendamento.Date + HoraAgendamento;
            agendamentoEmEdicao.Status = Status;

            await _viewModel.EditarAgendamento(agendamentoEmEdicao);

            await DisplayAlert("Sucesso", "Agendamento atualizado com sucesso!", "OK");

            agendamentoEmEdicao = null;
        }

        PickerCliente.SelectedIndex = -1;
        PickerFuncionario.SelectedIndex = -1;
        PickerServico.SelectedIndex = -1;
        PickerStatus.SelectedIndex = -1;

        DateAgendamento.Date = DateTime.Today;
        TimeAgendamento.Time = TimeSpan.Zero;

        LabelErro.IsVisible = false;

        ModalNovoAgendamento.IsVisible = false;
    }

    private void PickerFuncionario_SelectedIndexChanged(object sender, EventArgs e)
    {
        Funcionario funcionarioSelecionado = PickerFuncionario.SelectedItem as Funcionario;

        if (funcionarioSelecionado == null)
            return;

        foreach (Servico servico in _viewModel.Servicos)
        {
            if (servico.Nome_servico == funcionarioSelecionado.Nome_servico)
            {
                PickerServico.SelectedItem = servico;
                break;
            }
        }
    }
}





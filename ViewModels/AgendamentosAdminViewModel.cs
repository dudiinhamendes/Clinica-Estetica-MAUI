using System.Collections.ObjectModel;
using SistemaClinica.Models;
using SistemaClinica.Services;

namespace SistemaClinica.ViewModels;

public class AgendamentosAdminViewModel
{
    private readonly AgendamentoService _agendamentoService;
    private readonly ServicoService _servicoService;
    private readonly FuncionarioService _funcionarioService;

    public ObservableCollection<Agendamento> Agendamentos { get; set; }

    public ObservableCollection<Servico> Servicos { get; set; }

    public ObservableCollection<Funcionario> Funcionarios { get; set; }

    public AgendamentosAdminViewModel()
    {
        _agendamentoService = new AgendamentoService();
        _servicoService = new ServicoService();
        _funcionarioService = new FuncionarioService();

        Agendamentos = new ObservableCollection<Agendamento>();
        Servicos = new ObservableCollection<Servico>();
        Funcionarios = new ObservableCollection<Funcionario>();
    }

    public string ValidarCampos(string nomeCliente, string nomeServico, string nomeFuncionario, DateTime dataAgendamento, TimeSpan horaAgendamento, string status)
    {
        if (string.IsNullOrEmpty(nomeCliente) || string.IsNullOrEmpty(nomeServico) || string.IsNullOrEmpty(nomeFuncionario) || string.IsNullOrEmpty(status))
        {
            return "Obrigatório preencher todos os campos.";
        }

        return string.Empty;
    }

    public async Task<bool> CadastrarAgendamento(string nomeCliente, string nomeServico, string nomeFuncionario, DateTime dataAgendamento, TimeSpan horaAgendamento, string status)
    {
        try
        {
            DateTime dataHorario = dataAgendamento.Date + horaAgendamento;

            Agendamento novoAgendamento = new Agendamento
            {
                Nome_cliente = nomeCliente,
                Nome_servico = nomeServico,
                Nome_funcionario = nomeFuncionario,
                Data_Horario = dataHorario,
                Status = status
            };

            await _agendamentoService.AdicionarAgendamento(novoAgendamento);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task CarregarAgendamentos()
    {
        Agendamentos.Clear();

        var lista = await _agendamentoService.ListarAgendamentos();

        foreach (var agendamento in lista)
        {
            Agendamentos.Add(agendamento);
        }
    }

    public async Task CarregarServicos()
    {
        Servicos.Clear();

        var lista = await _servicoService.ListarServicos();

        foreach (var servico in lista)
        {
            Servicos.Add(servico);
        }
    }

    public async Task CarregarFuncionarios()
    {
        Funcionarios.Clear();

        var lista = await _funcionarioService.ListarFuncionarios();

        foreach (var funcionario in lista)
        {
            Funcionarios.Add(funcionario);
        }
    }

    public async Task EditarAgendamento(Agendamento agendamento)
    {
        await _agendamentoService.AtualizarAgendamento(agendamento);

        await CarregarAgendamentos();
    }

    public async Task ExcluirAgendamento(Agendamento agendamento)
    {
        await _agendamentoService.ExcluirAgendamento(
            agendamento.Id_agendamento);

        Agendamentos.Remove(agendamento);
    }
}
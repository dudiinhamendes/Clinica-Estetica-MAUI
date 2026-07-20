using System.Collections.ObjectModel;
using SistemaClinica.Models;
using SistemaClinica.Services;

namespace SistemaClinica.ViewModels;

public class AgendamentoViewModel
{
    private readonly AgendamentoService _agendamentoService;
    private readonly ServicoService _servicoService;
    private readonly FuncionarioService _funcionarioService;

    public ObservableCollection<Servico> Servicos { get; set; }
    public ObservableCollection<Funcionario> Funcionarios { get; set; }

    public AgendamentoViewModel()
    {
        _agendamentoService = new AgendamentoService();
        _servicoService = new ServicoService();
        _funcionarioService = new FuncionarioService();

        Servicos = new ObservableCollection<Servico>();
        Funcionarios = new ObservableCollection<Funcionario>();
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

    public string ValidarCampos(Servico servico, Funcionario funcionario, DateTime data, TimeSpan hora)
    {

        if (servico == null)
            return "Selecione um serviço.";

        if (funcionario == null)
            return "Nenhum profissional encontrado.";

        if (data == default)
            return "Selecione uma data.";

        return string.Empty;
    }

    public async Task<bool> CadastrarAgendamento(Servico servico, Funcionario funcionario, DateTime data, TimeSpan hora)
    {
        try
        {
            DateTime dataHorario = data.Date + hora;

            var agendamentos = await _agendamentoService.ListarAgendamentos();

            bool horarioOcupado = agendamentos.Any(a => a.Nome_funcionario == funcionario.Nome_funcionario && a.Data_Horario == dataHorario);

            if (horarioOcupado)
            {
                return false;
            }

            Agendamento novo = new Agendamento
            {
                Nome_cliente = SessaoUsuario.Nome,
                Nome_servico = servico.Nome_servico,
                Nome_funcionario = funcionario.Nome_funcionario,
                Data_Horario = dataHorario,
                Status = "Pendente"
            };

            await _agendamentoService.AdicionarAgendamento(novo);

            return true;
            
        } catch
        {
            return false;
        }
    }

    public async Task<List<Agendamento>> ListarAgendamentos()
    {
        return await _agendamentoService.ListarAgendamentos();
    }
}
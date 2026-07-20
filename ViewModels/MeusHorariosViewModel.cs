using System.Collections.ObjectModel;
using SistemaClinica.Models;
using SistemaClinica.Services;

namespace SistemaClinica.ViewModels;
public class MeusHorariosViewModel
{
    private readonly AgendamentoService _agendamentoService;
    public ObservableCollection<Agendamento> MeusAgendamentos { get; set; } = new();
    public MeusHorariosViewModel()
    {
        _agendamentoService = new AgendamentoService();

        MeusAgendamentos = new ObservableCollection<Agendamento>();

    }
    public async Task CarregarMeusAgendamentos()
    {
        var lista = await _agendamentoService.ListarAgendamentos();

        lista = lista.Where(a => a.Nome_cliente == SessaoUsuario.Nome).OrderBy(a => a.Data_Horario).ToList();

        MeusAgendamentos.Clear();

        foreach (var agendamento in lista)
        {
            MeusAgendamentos.Add(agendamento);
        }
    }

    public async Task ExcluirAgendamento(Agendamento agendamento)
    {
        await _agendamentoService.ExcluirAgendamento(agendamento.Id_agendamento);

        await CarregarMeusAgendamentos();
    }
}

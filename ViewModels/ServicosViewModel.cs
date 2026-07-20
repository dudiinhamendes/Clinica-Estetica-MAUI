using System.Collections.ObjectModel;
using SistemaClinica.Models;
using SistemaClinica.Services;

namespace SistemaClinica.ViewModels;
public class ServicosViewModel
{
    private readonly ServicoService _servicoService;
    private List<Servico> _todosServicos = new();
    public ObservableCollection<Servico> Servicos { get; set; }
    public ServicosViewModel()
    {
        _servicoService = new ServicoService();

        Servicos = new ObservableCollection<Servico>();
    }

    public async Task CarregarServicos()
    {
        _todosServicos = await _servicoService.ListarServicos();

        Servicos.Clear();

        foreach (var servico in _todosServicos)
        {
            Servicos.Add(servico);
        }
    }

    public void PesquisarServico(string texto)
    {
        Servicos.Clear();

        var listaFiltrada = string.IsNullOrWhiteSpace(texto) ? _todosServicos: _todosServicos.Where(s =>s.Nome_servico.Contains(texto, StringComparison.OrdinalIgnoreCase));

        foreach (var servico in listaFiltrada)
        {
            Servicos.Add(servico);
        }
    }
}
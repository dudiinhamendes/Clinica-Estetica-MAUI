using System.Collections.ObjectModel;
using SistemaClinica.Services;
using SistemaClinica.Models;

namespace SistemaClinica.ViewModels;

public class GerenciarFuncionarioViewModel
{
    private readonly FuncionarioService _funcionarioService;

    private readonly ServicoService _servicoService;

    public ObservableCollection<Funcionario> Funcionarios { get; set; }

    public ObservableCollection<Servico> Servicos { get; set; }

    public GerenciarFuncionarioViewModel()
    {
        _funcionarioService = new FuncionarioService();

        _servicoService = new ServicoService();

        Funcionarios = new ObservableCollection<Funcionario>();

        Servicos = new ObservableCollection<Servico>();
    }

    public string ValidarCampos(string NomeFuncionario, string Especialidade, string DisponibilidadeTexto)
    {
        if (string.IsNullOrEmpty(NomeFuncionario) || string.IsNullOrEmpty(Especialidade) || string.IsNullOrEmpty(DisponibilidadeTexto))
        {
            return "Obrigatório preencher todos os campos.";
        }

        return string.Empty;
    }

    public async Task<bool> CadastrarFuncionario(string nomeFuncionario, string nomeServico, bool disponivel) {
        try
        {
            Funcionario novoFuncionario = new Funcionario
            {
                Nome_funcionario = nomeFuncionario, Nome_servico = nomeServico, Disponivel = disponivel
            };

            await _funcionarioService.AdicionarFuncionario(novoFuncionario);

            return true;
        
        } catch
        {
            return false;
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

    public async Task CarregarServicos()
    {
        Servicos.Clear();

        var lista = await _servicoService.ListarServicos();

        foreach (var servico in lista)
        {
            Servicos.Add(servico);
        }
    }

    public async Task ExcluirFuncionario(Funcionario funcionario)
    {
        await _funcionarioService.ExcluirFuncionario(funcionario.Id_funcionario);

        Funcionarios.Remove(funcionario);
    }

    public async Task EditarFuncionario(Funcionario funcionario)
    {
        await _funcionarioService.AtualizarFuncionario(funcionario);

        await CarregarFuncionarios();
    }
}

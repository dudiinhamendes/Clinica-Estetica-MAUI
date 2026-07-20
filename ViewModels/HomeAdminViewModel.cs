using System.ComponentModel;
using System.Runtime.CompilerServices;
using SistemaClinica.Services;

namespace SistemaClinica.ViewModels;
public class HomeAdminViewModel : INotifyPropertyChanged
{
    private readonly AgendamentoService _agendamentoService;
    private readonly FuncionarioService _funcionarioService;
    private readonly ServicoService _servicoService;
    private readonly UsuarioService _usuarioService;

    public HomeAdminViewModel()
    {
        _agendamentoService = new AgendamentoService();

        _funcionarioService = new FuncionarioService();

        _servicoService = new ServicoService();

        _usuarioService = new UsuarioService();
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? nome = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }

    private int _totalAgendamentos;
    public int TotalAgendamentos
    {
        get => _totalAgendamentos; set
        {
            _totalAgendamentos = value;
            OnPropertyChanged();
        }
    }

    private int _totalFuncionarios;
    public int TotalFuncionarios
    {
        get => _totalFuncionarios; set
        {
            _totalFuncionarios = value;
            OnPropertyChanged();
        }
    }

    private int _totalServicos;
    public int TotalServicos
    {
        get => _totalServicos; set
        {
            _totalServicos = value;
            OnPropertyChanged();
        }
    }

    private int _totalUsuarios;
    public int TotalUsuarios
    {
        get => _totalUsuarios; set
        {
            _totalUsuarios = value;
            OnPropertyChanged();
        }
    }

    public async Task CarregarResumo()
    {
        TotalAgendamentos = (await _agendamentoService.ListarAgendamentos()).Count;

        TotalFuncionarios = (await _funcionarioService.ListarFuncionarios()).Count;

        TotalServicos = (await _servicoService.ListarServicos()).Count;

    }
}
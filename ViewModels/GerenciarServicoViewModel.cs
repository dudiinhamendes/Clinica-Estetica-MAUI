using SistemaClinica.Services;
using SistemaClinica.Models;
using System.Collections.ObjectModel;

namespace SistemaClinica.ViewModels;
public class GerenciarServicoViewModel 
{
    private readonly ServicoService _servicoService;
    public ObservableCollection<Servico> Servicos { get; set; }
    public GerenciarServicoViewModel()
    {

        _servicoService = new ServicoService();

        Servicos = new ObservableCollection<Servico>();
    }

    public string ValidarCampos(string NomeServico, string PrecoStr, string DuracaoStr)
    {
        if (string.IsNullOrEmpty(NomeServico) || string.IsNullOrEmpty(PrecoStr) || string.IsNullOrEmpty(DuracaoStr))
        {
            return "Obrigatório preencher todos os campos.";
        }

        if (!decimal.TryParse(PrecoStr, out decimal precoConvertido) || precoConvertido <= 0)
        {
            return "Preço inválido.";
        }

        if (!int.TryParse(DuracaoStr, out int duracaoConvertida) || duracaoConvertida <= 0)
        {
            return "Duração inválida.";
        }

        return string.Empty;
    }
    public async Task<bool> CadastrarServico(string nomeServico, decimal preco, int duracao)
    {
        try
        {
            Servico novoServico = new Servico
            {
                Nome_servico = nomeServico, Preco = preco, Duracao = duracao
            };

            await _servicoService.AdicionarServico(novoServico);

            return true;

        } catch
        {
            return false;
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

    public async Task ExcluirServico(int id)
    {
        await _servicoService.ExcluirServico(id);

        await CarregarServicos();
    }

    public async Task EditarServico(Servico servico)
    {
        await _servicoService.EditarServico(servico);

        await CarregarServicos();
    }
}
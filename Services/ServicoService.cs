using System.Collections.Generic;
using System.Text.Json;
using SistemaClinica.Models; 

namespace SistemaClinica.Services;

public class ServicoService {

 private readonly string caminhoArquivo;

 public ServicoService() 
 {
 caminhoArquivo = Path.Combine(FileSystem.AppDataDirectory, "servicos.json");
}

private async Task CriarArquivoSeNaoExistir() {
     if (!File.Exists(caminhoArquivo))
        {
            await File.WriteAllTextAsync(caminhoArquivo, "[]");
        }
}

public async Task<List<Servico>> ListarServicos()
{
    await CriarArquivoSeNaoExistir();

    string json = await File.ReadAllTextAsync(caminhoArquivo);

    var servicos = JsonSerializer.Deserialize<List<Servico>>(json);

    return servicos ?? new List<Servico>();
}

public async Task AdicionarServico(Servico servico)
{
    List<Servico> servicos = await ListarServicos();

    if (servicos.Count > 0)
    {
        servico.Id_servico = servicos.Max(s => s.Id_servico) + 1;
    }
    else
    {
        servico.Id_servico = 1;
    }

    servicos.Add(servico);

    string json = JsonSerializer.Serialize(servicos, new JsonSerializerOptions{WriteIndented = true});

    await File.WriteAllTextAsync(caminhoArquivo, json);
}

public async Task ExcluirServico(int id)
{
    List<Servico> servicos = await ListarServicos();

    Servico? servico = servicos.FirstOrDefault(s => s.Id_servico == id);

    if (servico != null)
    {
        servicos.Remove(servico);

        string json = JsonSerializer.Serialize(servicos, new JsonSerializerOptions{WriteIndented = true});

        await File.WriteAllTextAsync(caminhoArquivo, json);
    }
}

public async Task EditarServico(Servico servicoAtualizado)
{
    List<Servico> servicos = await ListarServicos();

    Servico? servico = servicos.FirstOrDefault(s => s.Id_servico == servicoAtualizado.Id_servico);

    if (servico != null)
    {
        servico.Nome_servico = servicoAtualizado.Nome_servico;
        servico.Preco = servicoAtualizado.Preco;
        servico.Duracao = servicoAtualizado.Duracao;

        string json = JsonSerializer.Serialize(servicos, new JsonSerializerOptions{WriteIndented = true});

        await File.WriteAllTextAsync(caminhoArquivo, json);
    }
}

}





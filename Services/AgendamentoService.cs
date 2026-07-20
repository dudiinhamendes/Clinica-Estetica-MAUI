using System.Text.Json;
using SistemaClinica.Models;

namespace SistemaClinica.Services;

public class AgendamentoService
{
    private readonly string caminhoArquivo;

    public AgendamentoService()
    {
        caminhoArquivo = Path.Combine(FileSystem.AppDataDirectory, "agendamentos.json");
    }

    private async Task CriarArquivoSeNaoExistir()
    {
        if (!File.Exists(caminhoArquivo))
        {
            await File.WriteAllTextAsync(caminhoArquivo, "[]");
        }
    }

    public async Task<List<Agendamento>> ListarAgendamentos()
    {
        await CriarArquivoSeNaoExistir();

        string json = await File.ReadAllTextAsync(caminhoArquivo);

        var agendamentos = JsonSerializer.Deserialize<List<Agendamento>>(json);

        return agendamentos ?? new List<Agendamento>();
    }

    public async Task AdicionarAgendamento(Agendamento agendamento)
    {
        List<Agendamento> agendamentos = await ListarAgendamentos();

        if (agendamentos.Count > 0)
        {
            agendamento.Id_agendamento = agendamentos.Max(a => a.Id_agendamento) + 1;
        }
        else
        {
            agendamento.Id_agendamento = 1;
        }

        agendamentos.Add(agendamento);

        string json = JsonSerializer.Serialize(agendamentos,new JsonSerializerOptions{WriteIndented = true});

        await File.WriteAllTextAsync(caminhoArquivo, json);
    }

    public async Task AtualizarAgendamento(Agendamento agendamentoAtualizado)
    {
        List<Agendamento> agendamentos = await ListarAgendamentos();

        int indice = agendamentos.FindIndex(a => a.Id_agendamento == agendamentoAtualizado.Id_agendamento);

        if (indice != -1)
        {
            agendamentos[indice] = agendamentoAtualizado;

            string json = JsonSerializer.Serialize(agendamentos, new JsonSerializerOptions{WriteIndented = true});

            await File.WriteAllTextAsync(caminhoArquivo, json);
        }
    }

    public async Task ExcluirAgendamento(int id)
    {
        List<Agendamento> agendamentos = await ListarAgendamentos();

        agendamentos.RemoveAll(a => a.Id_agendamento == id);

        string json = JsonSerializer.Serialize(agendamentos,new JsonSerializerOptions {WriteIndented = true});

        await File.WriteAllTextAsync(caminhoArquivo, json);
    }
}
using System.Text.Json;
using SistemaClinica.Models;

namespace SistemaClinica.Services;

public class FuncionarioService
{
    private readonly string caminhoArquivo;

    public FuncionarioService()
    {
        caminhoArquivo = Path.Combine(FileSystem.AppDataDirectory, "funcionarios.json");
    }

    private async Task CriarArquivoSeNaoExistir()
    {
        if (!File.Exists(caminhoArquivo))
        {
            await File.WriteAllTextAsync(caminhoArquivo, "[]");
        }
    }

    public async Task<List<Funcionario>> ListarFuncionarios()
    {
        await CriarArquivoSeNaoExistir();

        string json = await File.ReadAllTextAsync(caminhoArquivo);

        var funcionarios = JsonSerializer.Deserialize<List<Funcionario>>(json);

        return funcionarios ?? new List<Funcionario>();
    }

    public async Task AdicionarFuncionario(Funcionario funcionario)
    {
        List<Funcionario> funcionarios = await ListarFuncionarios();

        if (funcionarios.Count > 0)
        {
            funcionario.Id_funcionario = funcionarios.Max(f => f.Id_funcionario) + 1;
        }
        else
        {
            funcionario.Id_funcionario = 1;
        }

        funcionarios.Add(funcionario);

        string json = JsonSerializer.Serialize(funcionarios, new JsonSerializerOptions {WriteIndented = true});

        await File.WriteAllTextAsync(caminhoArquivo, json);
    }

    public async Task AtualizarFuncionario(Funcionario funcionarioAtualizado)
    {
        List<Funcionario> funcionarios = await ListarFuncionarios();

        int indice = funcionarios.FindIndex(f => f.Id_funcionario == funcionarioAtualizado.Id_funcionario);

        if (indice != -1)
        {
            funcionarios[indice] = funcionarioAtualizado;

            string json = JsonSerializer.Serialize(funcionarios, new JsonSerializerOptions{ WriteIndented = true});

            await File.WriteAllTextAsync(caminhoArquivo, json);
        }
    }

    public async Task ExcluirFuncionario(int id)
    {
        List<Funcionario> funcionarios = await ListarFuncionarios();

        funcionarios.RemoveAll(f => f.Id_funcionario == id);

        string json = JsonSerializer.Serialize(funcionarios, new JsonSerializerOptions{WriteIndented = true});

        await File.WriteAllTextAsync(caminhoArquivo, json);
    }
}
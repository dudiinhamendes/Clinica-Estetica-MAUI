using System.Collections.Generic;
using SistemaClinica.Models;

namespace SistemaClinica.Models;

public class Funcionario
{

    public int Id_funcionario { get; set; }

    public string Nome_funcionario { get; set; }

    public string Nome_servico { get; set; }

    public bool Disponivel { get; set; }

    public string StatusDisponibilidade
    {
        get
        {
            return Disponivel ? "Disponível" : "Indisponível";
        }
    }

    public Funcionario()
    {
    }

    public Funcionario(int id_func, string nome_func, string nomeServico, bool disponivel)
    {
        Id_funcionario = id_func;
        Nome_funcionario = nome_func;
        Nome_servico = nomeServico;
        Disponivel = disponivel;
    }
}
using System.Collections.Generic;
using SistemaClinica.Models;

namespace SistemaClinica.Models;

public class Servico
{

    public int Id_servico { get; set; }

    public string Nome_servico { get; set; }

    public decimal Preco { get; set; }

    public int Duracao { get; set; }

    public Servico() 
    {
    }

    public Servico(int id_ser, string nome_ser, decimal preco, int duracao)
    {
        Id_servico = id_ser;
        Nome_servico = nome_ser;
        Preco = preco;
        Duracao = duracao;
    }
}
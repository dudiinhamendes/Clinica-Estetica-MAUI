using System.Collections.Generic; 
using SistemaClinica.Models;

namespace SistemaClinica.Models;

public class Usuario
{

    public int Id_usuario { get; set; }

    public string Nome_usuario { get; set; }

    public string Email { get; set; }

    public string Senha { get; set; }

    public string CPF { get; set; }

    public string Telefone {get; set; }
    
    public string Tipo { get; set; }

    public Usuario(int id_usu, string nome_usu, string email, string senha, string cpf, string telefone, string tipo)
    {
        Id_usuario = id_usu;
        Nome_usuario = nome_usu;
        Email = email;
        Senha = senha;
        CPF = cpf;
        Telefone = telefone;
        Tipo = tipo;
    }
}
using SistemaClinica.Services;
using SistemaClinica.Models;

namespace SistemaClinica.ViewModels;

public class CadastroViewModel 
{
    private readonly UsuarioService _usuarioService;
    public CadastroViewModel()
    {
        _usuarioService = new UsuarioService();
    }
    public string ValidarCampos(string nomeCompleto, string cpf, string telefone, string email, string senha, string confirmarSenha)
    {
        if (string.IsNullOrEmpty(nomeCompleto) || string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(confirmarSenha))
        {
            return "Obrigatório preencher todos os campos.";
        }

        string cpfLimpo = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
        {
            return "CPF deve conter 11 dígitos.";
        }

        if (!email.Contains("@"))
        {
            return "Digite um e-mail válido.";
        }

        if (senha.Length < 6)
        {
            return "A senha deve ter pelo menos 6 caracteres.";
        }

        if (senha != confirmarSenha)
        {
            return "As senhas não coincidem.";
        }

        string telefoneLimpo = telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

        return string.Empty;
    }
   public bool CadastrarUsuario(string nomeCompleto, string cpf, string telefone, string email, string senha) {
        try
        {
            int proximoId = _usuarioService.ListarUsuario().Count + 1;
            string tipoPadrao = "Comum";

            Usuario novoUsuario = new Usuario(proximoId, nomeCompleto, email, senha, cpf, telefone, tipoPadrao);

            _usuarioService.CadastrarUsuario(novoUsuario);
            return true;
       
        } catch
        {
            return false;
        }

    }
}


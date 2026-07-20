namespace SistemaClinica.ViewModels;
public class LoginViewModel
{
    public bool AutenticarUsuario(string email, string senha)
    {
        return email == "aluno@teste.com" && senha == "123456";
    }
    public string ValidarCampos(string email, string senha)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            return "Obrigatório preencher todos os campos.";
        }

        if (!email.Contains("@"))
        {
            return "Digite um e-mail válido.";
        }

        if (senha.Length < 6)
        {
            return "A senha deve ter pelo menos 6 caracteres.";
        }

        return "";
    }
}
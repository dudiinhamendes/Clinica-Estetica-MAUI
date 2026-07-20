using System.Collections.Generic;
using SistemaClinica.Models;

namespace SistemaClinica.Services;

public class UsuarioService
{
  private List<Usuario> usuarios = new List<Usuario>();

  public void CadastrarUsuario(Usuario usuario)
  {
    usuarios.Add(usuario);
  }

  public List<Usuario> ListarUsuario()
  {
    return usuarios;
  }

}
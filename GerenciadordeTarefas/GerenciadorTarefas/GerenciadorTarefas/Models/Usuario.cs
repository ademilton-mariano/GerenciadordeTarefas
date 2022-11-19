using GerenciadorTarefas.ViewModels;
using SecureIdentity.Password;

namespace GerenciadorTarefas.Models;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
    public int Idade { get; private set; }
    public string? Endereco { get; private set; }
    public string? Bio { get; private set; }
    public virtual IList<Tarefa>? Tarefas { get; private set; }

    public void CadastrarUsuario(CadastroUsuarioViewModel model)
    {
        Nome = model.Nome;
        Email = model.Email.ToLower();
        Senha = PasswordHasher.Hash(model.Senha);
        Idade = model.Idade;
        Endereco = model.Endereco;
        Bio = model.Bio;
    }
    
    public void EditarUsuario(EditarUsuarioViewModel model)
    {
        Nome = model.Nome;
        Email = model.Email.ToLower();
        Idade = model.Idade;
        Endereco = model.Endereco;
        Bio = model.Bio;
    }
}    
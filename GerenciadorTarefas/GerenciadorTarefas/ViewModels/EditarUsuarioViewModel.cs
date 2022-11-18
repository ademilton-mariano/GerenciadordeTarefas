using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.ViewModels;

public class EditarUsuarioViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail é inválido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "A senha é obrigatória")]
    public int Idade { get; set; }
    public string? Endereco { get; set; }
    public string? Bio { get; set; }
}
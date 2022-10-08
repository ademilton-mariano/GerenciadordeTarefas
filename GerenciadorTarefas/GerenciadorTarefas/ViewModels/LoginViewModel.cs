using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail é inválido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Informe a senha")]
    public string Senha { get; set; }
}
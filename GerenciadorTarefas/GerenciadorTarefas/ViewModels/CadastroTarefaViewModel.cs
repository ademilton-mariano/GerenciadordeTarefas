using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.ViewModels;

public class CadastroTarefaViewModel
{
    [Required(ErrorMessage = "A Descrição é obrigatória")]
    public string Descricao { get; set; }
    [Required(ErrorMessage = "A Data é obrigatória")]
    public string Data { get; set; }
    [Required(ErrorMessage = "A Data é obrigatória")]
    public string Hora { get; set; }
}
using GerenciadorTarefas.ViewModels;

namespace GerenciadorTarefas.Models;

public class Tarefa
{
    public int Id { get; private set; }
    public string Descricao { get; private set; }
    public string Data { get; private set; }
    public string Hora { get; private set; }
    public Usuario Usuario { get; set; } 
    
    public void CadastrarOuEditarTarefa(CadastroTarefaViewModel model)
    {
        Descricao = model.Descricao;
        Data = model.Data;
        Hora = model.Hora;
    }
}

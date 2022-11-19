using GerenciadorTarefas.Data;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Controllers;

[Authorize]
[ApiController]
public class TarefaController : ControllerBase
{
    [HttpGet("usuarios/{idUsuario:int}/tarefas")]
    public IActionResult ListarTarefas([FromServices] DataContext context,[FromRoute] int idUsuario )
    {
        try
        {
            var tarefas = context.Tarefas.Where(p => p.Usuario.Id == idUsuario);
            return Ok(tarefas);
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }

    [HttpGet("usuarios/{idUsuario:int}/tarefas/{id:int}")]
    public IActionResult ListarUmaTarefa([FromServices] DataContext context, [FromRoute] int id)
    {
        try
        {
            var tarefa = context.Tarefas.FirstOrDefault(x => x.Id == id);

            if (tarefa == null)
                return BadRequest("Conteúdo não encontrado");

            return Ok(tarefa);
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }

    [HttpPost("usuarios/{idUsuario:int}/tarefas")]
    public IActionResult CadastrarTarefa([FromBody] CadastroTarefaViewModel model,
        [FromRoute] int idUsuario, 
        [FromServices] DataContext context)
    {
        try
        {
            var xUsuario = context.Usuarios.FirstOrDefault(x => x.Id == idUsuario);
            
            if (xUsuario == null)
                return BadRequest("Conteúdo não encontrado");
            
            var xTarefa = new Tarefa();
            xTarefa.CadastrarOuEditarTarefa(model);
            xTarefa.Usuario = xUsuario;
            context.Tarefas.Update(xTarefa);
            context.SaveChanges();
            return Created($"usuarios/{idUsuario}/tarefas/{xTarefa.Id}", "Criada com Sucesso");
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Não foi possível adicionar a tarefa");
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }

    [HttpPut("usuarios/{idUsuario:int}/tarefas/{id:int}")]
    public IActionResult EditarTarefa([FromBody] CadastroTarefaViewModel model,
        [FromRoute] int id, 
        [FromRoute] int idUsuario,
        [FromServices] DataContext context)
    {
        try
        {
            var xTarefa = context.Tarefas.FirstOrDefault(x => x.Id == id && x.Usuario.Id == idUsuario);
            
            if(xTarefa == null)
                return BadRequest("Conteúdo não encontrado");
            
            xTarefa.CadastrarOuEditarTarefa(model);
            context.Tarefas.Update(xTarefa);
            context.SaveChanges();
            return Ok("Editado com sucesso");
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Não foi possível editar o usuário");
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }
    
    [HttpDelete("usuarios/{idUsuario:int}/tarefas/{id:int}")]
    public IActionResult ApagarTarefa([FromRoute] int id,[FromRoute] int idUsuario, [FromServices] DataContext context)
    {
        try
        {
            var xTarefa = context.Tarefas.FirstOrDefault(x => x.Id == id && x.Usuario.Id == idUsuario);
            
            if(xTarefa == null)
                return BadRequest("Conteúdo não encontrado");
            
            context.Tarefas.Remove(xTarefa);
            context.SaveChanges();
            return Ok("Removido com sucesso");
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Não foi possível excluir o usuário");
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }
}

﻿using GerenciadorTarefas.Data;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GerenciadorTarefas.Controllers;

[ApiController]
public class CadastroUsuarioController : ControllerBase
{
    [HttpGet("usuarios")]
    public IActionResult Get([FromServices] DataContext context)
    {
        try
        {
            var xUsuarios = context.Usuarios.ToList();
            return Ok(xUsuarios);
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }

    [HttpGet("usuarios/{id:int}")]
    public IActionResult GetById([FromServices] DataContext context, [FromRoute] int id)
    {
        try
        {
            var xUsuario = context.Usuarios.FirstOrDefault(x => x.Id == id);

            if (xUsuario == null)
                return BadRequest("Conteúdo não encontrado");

            return Ok(xUsuario);
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }
    
    [HttpPost("usuarios")]
    public IActionResult Post([FromBody] CadastroUsuarioViewModel model,[FromServices] DataContext context)
    {
        try
        {
            var xUsuario = new Usuario();
            xUsuario.CadastrarOuEditarUsuario(model);
            context.Usuarios.Add(xUsuario);
            context.SaveChanges();
            return Created($"usuarios/{xUsuario.Id}", "Criado com Sucesso");
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Não foi possível adicionar o usuário");
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }

    [HttpPut("usuarios/{id:int}")]
    public IActionResult Put([FromBody] CadastroUsuarioViewModel model,
        [FromRoute] int id,
        [FromServices] DataContext context)
    {
        try
        {
            var xUsuario = context.Usuarios.FirstOrDefault(x => x.Id == id);
            
            if (xUsuario == null)
                return BadRequest("Conteúdo não encontrado");
            
            xUsuario.CadastrarOuEditarUsuario(model);
            context.Usuarios.Update(xUsuario);
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
    
    [HttpDelete("usuarios/{id:int}")]
    public IActionResult Delete([FromRoute] int id, [FromServices] DataContext context)
    {
        try
        {
            var xUsuario = context.Usuarios.FirstOrDefault(x => x.Id == id);

            if (xUsuario == null)
                return BadRequest("Conteúdo não encontrado");

            context.Usuarios.Remove(xUsuario);
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
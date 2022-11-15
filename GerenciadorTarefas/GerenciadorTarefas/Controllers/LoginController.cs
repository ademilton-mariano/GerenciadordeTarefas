using GerenciadorTarefas.Data;
using GerenciadorTarefas.Services;
using GerenciadorTarefas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace GerenciadorTarefas.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Logar([FromBody] LoginViewModel model, 
        [FromServices] DataContext context,
        [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var usuario =  context.Usuarios.AsNoTracking()
                .FirstOrDefault(x => x.Email == model.Email.ToLower());

            if (usuario == null)
                return StatusCode(401, "Usuário ou senha inválido");

            if (!PasswordHasher.Verify(usuario.Senha, model.Senha))
                return StatusCode(401,"Usuário ou senha inválido");

            var token = tokenService.GenerateToken(usuario);
            return Ok(token);
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }
}

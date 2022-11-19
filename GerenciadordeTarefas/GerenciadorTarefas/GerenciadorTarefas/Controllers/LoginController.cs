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
    public ActionResult<dynamic> Logar([FromBody] LoginViewModel model, 
        [FromServices] DataContext context,
        [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var xUsuario =  context.Usuarios.AsNoTracking()
                .FirstOrDefault(x => x.Email == model.Email.ToLower());

            if (xUsuario == null)
                return StatusCode(401, "Usuário ou senha inválido");

            if (!PasswordHasher.Verify(xUsuario.Senha, model.Senha))
                return StatusCode(401,"Usuário ou senha inválido");

            var token = tokenService.GenerateToken(xUsuario);
            return new
            {
                Id = xUsuario.Id,
                Nome = xUsuario.Nome,
                Email = xUsuario.Email,
                Idade = xUsuario.Idade,
                Endereco = xUsuario.Endereco,
                Bio = xUsuario.Bio,
                Token = token
            };
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }
}

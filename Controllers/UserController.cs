using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.DDD.AspNetCore.Data;
using ShopApi.DDD.AspNetCore.Model;
using ShopApi.DDD.AspNetCore.Services;

namespace ShopApi.DDD.AspNetCore.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post(
            [FromServices] DataContext context,
            [FromBody] User model
        )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                // Força o usuário a ser sempre "funcionario"
                model.Role = "employee";


                context.Users.Add(model);
                await context.SaveChangesAsync();

                // Esconde a senha

                model.Password = "";
                return model;
            }
            catch(Exception)
            {
                return BadRequest(new { message = "Não foi possivel criar o usuário"});
            }    
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] User model
        )
        {
            var user = await context.Users
                .AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if(user == null)
                return BadRequest(new { message = "Usuário ou senha inválido"});

            var token = TokenService.GenerateToken(user);

            user.Password = "";
            return new
            {
                user = user,
                token = token
            };        
        }
    }
}
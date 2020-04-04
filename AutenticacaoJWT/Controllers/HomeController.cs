using AutenticacaoJWT.Models;
using AutenticacaoJWT.Repositories;
using AutenticacaoJWT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace AutenticacaoJWT.Controllers
{

    [Route("v1/account")]
    public class HomeController: ControllerBase
    {
        [HttpPost]
        [Route("logar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {

            var user = UserRepository.Get(model.UserName, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuario ou senha invalido" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "Membro")]
        public string Employee() => String.Format("Funcionario");

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "Lider")]
        public string Manager() => String.Format("Gerente");
    }
}

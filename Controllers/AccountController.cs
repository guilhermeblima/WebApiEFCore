using System.Collections.Generic;
using System.Threading.Tasks;
using ApiEFCore.Data;
using ApiEFCore.Models;
using ApiEFCore.Repository;
using ApiEFCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEFCore.Controllers
{
    [ApiController]
    [Route("v1/accounts")]
    public class AccountController : ControllerBase
    {

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromBody] User model)
        {
            var user =  UserRepository.Get(model.Username, model.Password);

            if(user == null)
                return NotFound(new {message = "Username or password incorrect"});

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
        public string Anonymous() => "Anonymous";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => string.Format("Authenticated = {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Employee";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Manager";
    }
}
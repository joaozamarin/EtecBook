using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EtecBookAPI.Data;
using EtecBookAPI.DataTransferObjects;
using EtecBookAPI.Helpers;
using EtecBookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtecBookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO login)
        {
            if (login == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            AppUser user = new();

            if (isEmail(login.Email))
            {
                // Acha o user pelo email
                user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(login.Email));
            }
            else
            {
                // Acha o user pelo username
                user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(login.Email));
            }

            if (user == null)
                return NotFound(new { message = "Usuário e/ou Senha Inválidos!" });
            
            if (!PasswordHasher.VerifyPassword(login.Password, user.Password))
                return NotFound( new { message = "Usuário e/ou Senha Inválidos!" });
            
            return Ok(new { message = $"Usuário {user.Name} autenticado!" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            if (register == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            // Checar se o email já existe
            if (await _context.Users.AnyAsync(u => u.Email.Equals(register.Email)))
                return BadRequest(new { message = "Email já cadastrado! Tente recuperar sua senha ou entre em contato com os administradores" });
            
            // Checar a força da senha
            var pass = checkPasswordStrength(register.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { message = pass });
            
            //Criar o usuário

            return Ok();
        }

        private bool isEmail(string email)
        {
            try
            {
                MailAddress mail = new(email);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string checkPasswordStrength(string password)
        {
            StringBuilder sb = new();
            
            if (password.Length < 6)
                sb.Append("A Senha deve possuir no mínimo 6 caracteres " + Environment.NewLine);

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("A Senha deve ser alfanumérica " + Environment.NewLine);
            
            if (!Regex.IsMatch(password, "[<,>,!,@,#,$,%,&,*,(,),_,-,=,+,/,:,;,|,\\,?,{,},`,~,^,\\[,\\],."))
                sb.Append("A Senha deve conter um caractere especial " + Environment.NewLine);
            
            return sb.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EtecBookAPI.DataTransferObjects
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email ou Nome de Usuário requerido")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha requerida")]
        public string Password { get; set; }
    }
}
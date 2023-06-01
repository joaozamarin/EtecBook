using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EtecBookAPI.DataTransferObjects
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Informe o Nome")]
        [StringLength(60, ErrorMessage = "O Nome deve possuir no máximo 60 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe o Email")]
        [EmailAddress(ErrorMessage = "Informe um Email válido")]
        [StringLength(100, ErrorMessage = "O Email deve possuir no máximo 100 caracteres")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Informe a Senha")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A Senha deve possuir no mínimo 6 e no máximo 10 caracterers")]
        public string Password { get; set; }
    }
}
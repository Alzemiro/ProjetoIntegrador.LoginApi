using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF.WebApi.DTO
{
    public class UsuarioDtoForRegister
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(24 ,MinimumLength = 4, ErrorMessage = "A senha deve conter ao menos 4 digitos")]
        public string Password { get; set; }        
    }
}

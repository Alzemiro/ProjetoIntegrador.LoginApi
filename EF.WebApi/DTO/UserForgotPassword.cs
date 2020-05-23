using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF.WebApi.DTO
{
    public class UserForgotPassword
    {
        [EmailAddress]
        public string Email { get; set; }

    }
}

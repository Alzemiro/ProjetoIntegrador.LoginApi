using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF.WebApi.DTO
{
    public class UserForLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }       

    }
}

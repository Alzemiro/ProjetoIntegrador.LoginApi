using ProjetoV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF.WebApi.Data
{
    public interface IAuthRepository
    {
        Task<Usuario> Register(Usuario usuario, string password);
        Task<Usuario> Login(string username, string password);
        Task<bool> UserExists(string username, string email);
    }
}

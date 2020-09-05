using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjetoV.Models
{
    public class Usuario
    {
        public Usuario()
        {
            this.Date = DateTime.UtcNow;            
        }
        
        public Guid Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string User { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Date { get; set; }
    }
}

using EF.WebApi.Data;
using EF.WebApi.DTO;
using EF.WebApi.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EF.WebApi.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class UsuariosController : ControllerBase, IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public UsuariosController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("recover")]
        public async Task<IActionResult> Recover([FromBody]UserForgotPassword userForgot)
        {

            if (!await _context.Usuarios.AnyAsync(x => x.Email == userForgot.Email))
                return NotFound();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:ResetPassToken").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userForgot.Email)

                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var resetTokenString = tokenHandler.WriteToken(token);

            return Ok(new { resetTokenString });

        }




        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("recover")]
        public async Task<ActionResult<UserNewPasswordDto>> UpdatePassword([FromQuery(Name = "resetToken")]string resetToken, [FromBody]UserNewPasswordDto userNewPassword)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenRead = handler.ReadJwtToken(resetToken);

            var tkRead = tokenRead.Claims.ToList();

            var email = tkRead.Where(s => s.Type == "email").Select(c => c.Value).FirstOrDefault();

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(userNewPassword.Password, out passwordHash, out passwordSalt);

            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.Equals(email));
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;
            List<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(usuario);
            

           await _context.BulkUpdateAsync<Usuario>(usuarios);


            return Ok();
        }

        // POST: api/Register      
        [HttpPost("register")]
        public async Task<ActionResult<UsuarioDtoForRegister>> PostUsuario(UsuarioDtoForRegister usuarioDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                usuarioDto.Username = usuarioDto.Username.ToLower();

                if (await UserExists(usuarioDto.Username, usuarioDto.Email))
                    return BadRequest("Usuário já cadastrado");

                var userToCreate = new Usuario
                {
                    User = usuarioDto.Username,
                    Email = usuarioDto.Email
                };

                _ = await Register(userToCreate, usuarioDto.Password);


            }
            catch (Exception ex)
            {

                throw ex;
            }

            return StatusCode(201);


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLogin userForLogin)
        {
            var userFromRepo = await Login(userForLogin.Username.ToLower(), userForLogin.Password);
            if (userFromRepo == null)
                return Unauthorized();

            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.User)

                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });
        }




        public async Task<Usuario> Register(Usuario usuario, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        public async Task<Usuario> Login(string username, string password)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.User == username);
            if (user == null)
                return null;

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<bool> UserExists(string username, string email)
        {
            if (await _context.Usuarios.AnyAsync(x => x.User == username || x.Email == email))
                return true;
            return false;
        }
    }
}

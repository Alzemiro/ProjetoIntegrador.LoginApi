using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoV.database;
using ProjetoV.Models;

namespace ProjetoV.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    class TimeController : ControllerBase
    {

        private readonly ApplicationDBContext _context;

            public TimeController(ApplicationDBContext context)
            {
                _context = context;
            }

        [HttpGet("times")]
        public async Task<ActionResult<IEnumerable<Time>>> GetTimes()
        {
            return await _context.Times.ToListAsync();
        }

    }
}

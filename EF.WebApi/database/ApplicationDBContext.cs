using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProjetoV.Models;

namespace ProjetoV.database
{
    public class ApplicationDBContext : DbContext
    {
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=projeto;uid=root;password=admin");
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Campeonato> Campeonato { get; set; }
        public DbSet<Clube> Clubes { get; set; }
        public DbSet<Gol> Gols { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Rodada> Rodadas { get; set; }
        public DbSet<Temporada> Temporadas { get; set; }
        public DbSet<Time> Times { get; set; }
    }
}

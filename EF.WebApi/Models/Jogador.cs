using System;
using System.Collections.Generic;
using System.Text;
using ProjetoV.Models;

namespace ProjetoV.Models
{
    public class Jogador
    {
        public Guid JogadorId { get; set; }

        public string NomeJogador { get; set; }
        public string User { get; set; }

        public Guid GolId { get; set; }

        public Gol Gol { get; set; }

        public Guid TimeId { get; set; }

        public Time Time { get; set; }


    }
}

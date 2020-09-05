using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoV.Models
{
    public class Partida
    {
        public Guid PartidaId { get; set; }

        public string mandantePartida { get; set; }
        public string LocalPartida { get; set; }

        public DateTime DataHoraPartida { get; set; }

        public int placarFinal { get; set; }

        public Guid GolId { get; set; }

        public Gol Gol { get; set; }
    }
}

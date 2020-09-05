using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoV.Models
{
    public class Temporada
    {
        public Guid TemporadaId { get; set; }

        public int Etapa { get; set; }

        public Guid PartidaId { get; set; }

        public Partida Partida { get; set; }

        public Guid ClubeId { get; set; }

        public Clube Clube { get; set; }
    }
}

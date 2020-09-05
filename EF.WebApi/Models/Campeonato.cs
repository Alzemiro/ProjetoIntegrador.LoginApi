using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoV.Models
{
    public class Campeonato
    {
        public Guid CampeonatoId { get; set; }

        public string NomeCampeonato { get; set; }

        public Guid TemporadaId { get; set; }

        public Temporada Temporada { get; set; }
    }
}

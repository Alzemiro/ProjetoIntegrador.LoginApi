using System;
using System.Collections.Generic;
using System.Text;
using ProjetoV.Models;

namespace ProjetoV.Models
{
    public class Clube
    {
        public Guid ClubeId { get; set; }
        public string ClubeNome { get; set; }

        public string ClubeCidade { get; set; }

        public string ClubeEstadio { get; set; }

        public Guid TimeId { get; set; }

        public Time Time { get; set; }

        public Guid PartidaId { get; set; }

        public Partida Partida { get; set; }

    }
}

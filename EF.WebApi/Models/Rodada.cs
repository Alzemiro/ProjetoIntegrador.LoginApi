using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoV.Models
{
    public class Rodada
    {
        public Guid RodadaId { get; set; }
        public string ClubeNome { get; set; }
        public DateTime DataInicioRodada { get; set; }

        public DateTime DataFimRodada { get; set; }

        public Guid PartidaId { get; set; }

        public Partida Partida { get; set; }
    }
}

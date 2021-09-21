using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Local { get; set; }
        public DateTime DataEvento { get; set; }
        public string Tema { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int QtdPessoas { get; set; }
        public virtual IEnumerable<Lote> Lotes { get; set; } = new List<Lote>();
        public virtual IEnumerable<PalestranteEvento> Palestrantes { get; set; } = new List<PalestranteEvento>();
        public virtual IEnumerable<RedeSocial> RedeSocias { get; set; } = new List<RedeSocial>();
        public string ImagemUrl { get; set; }
    }
}

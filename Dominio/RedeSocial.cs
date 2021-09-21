using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Dominio
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public int? EventoId { get; set; }
        public virtual Evento Evento { get; }
        public int? PalestranteId { get; set; }
        public virtual Palestrante Palestrante { get; }
    }
}

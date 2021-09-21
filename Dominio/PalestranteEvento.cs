using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Dominio
{
    public class PalestranteEvento
    {
        [JsonIgnore]
        public int EventoId { get; set; }
        [JsonIgnore]
        public virtual Evento Evento { get; set; }
        public int PalestranteId { get; set; }
        public virtual Palestrante Palestrante { get; set; }
    }
}

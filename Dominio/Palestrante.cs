﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Dominio
{
     public class Palestrante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemUrl { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<RedeSocial> RedeSocias { get; set; } = new List<RedeSocial>();
        [JsonIgnore]
        public virtual IEnumerable<PalestranteEvento> Eventos { get; set; } = new List<PalestranteEvento>();
    }
}

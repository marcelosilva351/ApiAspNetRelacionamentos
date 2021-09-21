using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Lote
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? Datafim { get; set; }
        public int Quantidade { get; set; }
        public int EventoId { get; set; }
        public virtual Evento Evento { get; }
    }
}
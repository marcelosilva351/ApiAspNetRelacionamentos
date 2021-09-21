using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace Repository
{
    public class ProAgilRepository : IProagilRepository
    {
        private readonly ApiContext _context;

        public ProAgilRepository(ApiContext context)
        {
            _context = context;
        }

        public void Add<T>(T Entidade)
        {
            _context.Add(Entidade);
        }

        public void Delete<T>(T entidade)
        {
            _context.Remove(entidade);
        }

        public async Task<Evento> ObterEventoPorId(int id, bool includePalestrate)
        {

            var Evento = _context.Evento.Include(e => e.Lotes).Include(e => e.RedeSocias);
            if (includePalestrate)
            {
                Evento.Include(e => e.Palestrantes).ThenInclude(e => e.Palestrante);
            }
            return await Evento.FirstAsync(e => e.EventoId == id);

        }

        public async Task<IEnumerable<Evento>> ObterEventosAsync(bool includePalestrante)
        {
            var Eventos = _context.Evento.Include(e => e.Lotes)
                .Include(e => e.RedeSocias).Include
                (e => e.Palestrantes)
                .ThenInclude(p => p.Palestrante); 

    
            var OrderByDataEvento = Eventos.OrderByDescending(e => e.DataEvento);
            return await Eventos.ToListAsync();
        }

        public async Task<IEnumerable<Evento>> ObterEventosAsyncPorTema(string tema, bool includePalestramtes)
        {
            var Eventos = _context.Evento.Where(e => e.Tema == tema).Include(e => e.Lotes).Include(e => e.RedeSocias);

            if (includePalestramtes)
            {
                Eventos.Include(e => e.Palestrantes).ThenInclude(p => p.Palestrante);
            }
            var OrderByDataEvento = Eventos.OrderByDescending(e => e.DataEvento);
            return await Eventos.ToListAsync();
        }

        public async Task<Palestrante> ObterPalestrantePorIdAsync(int id, bool includeEventos)
        {
            var Palestrantes = _context.Palestrante.Include(p => p.RedeSocias);
            if (includeEventos)
            {
                Palestrantes.Include(p => p.Eventos).ThenInclude(p => p.Evento);
            }
            return await Palestrantes.FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Palestrante>> ObtertodosPalestrantesAsync(bool includeEventos)
        {
            var palestrantes = _context.Palestrante.Include(p => p.RedeSocias);
            if (includeEventos)
            {
                palestrantes.Include(p => p.Eventos).ThenInclude(p => p.Evento);
            }
            return await palestrantes.ToListAsync();


        }

        

        public async Task<bool> SaveChangesAsync()
        {
            return await (_context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T Entidade)
        {
            _context.Update(Entidade);
        }

        public IEnumerable<PalestranteEvento> AdicionarPalestranteEvento(Evento evento)
        {
            var eventos = _context.Evento.ToList();
            var palestrantes = _context.Palestrante.ToList();
            var palestrantesEventos = _context.PalestranteEvento.ToList();

            var join = from e in eventos
                       join pe in palestrantesEventos
                       on e.EventoId equals pe.EventoId
                       join p in palestrantes
                       on pe.PalestranteId equals p.Id
                       select new PalestranteEvento{ Evento = e, Palestrante = p };

            return join.ToList();

       

        }
    }
}

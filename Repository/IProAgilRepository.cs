using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProagilRepository
    {
        //Geral
        void Add<T>(T Entidade);
        void Update<T>(T Entidade);
        void Delete<T>(T entidade);
        Task<bool> SaveChangesAsync();

        //Evento
        Task<IEnumerable<Evento>> ObterEventosAsyncPorTema(string tema, bool includePalestramtes);

        Task<IEnumerable<Evento>> ObterEventosAsync(bool includePalestrante);

        Task<Evento> ObterEventoPorId(int id, bool includePalestrante);

        // Palestrante

        Task<IEnumerable<Palestrante>> ObtertodosPalestrantesAsync(bool includeEventos);
        Task<Palestrante> ObterPalestrantePorIdAsync(int id, bool includeEventos);




    }
}

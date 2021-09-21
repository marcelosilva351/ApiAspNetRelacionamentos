using AutoMapper;
using BackCursoAngularAsp.Data.DTO_s.Eventos;
using BackCursoAngularAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCursoAngularAsp.Profiles
{
    public class EventoProfile : Profile
    {
        public EventoProfile()
        {
            CreateMap<CreateEventoDTO, Evento>();
            CreateMap<Evento, ReadEventoDTO>();
        }
    }
}

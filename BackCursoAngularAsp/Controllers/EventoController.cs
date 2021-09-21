using AutoMapper;
using BackCursoAngularAsp.Data;
using BackCursoAngularAsp.Data.DTO_s.Eventos;
using BackCursoAngularAsp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCursoAngularAsp.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    public class EventoController : ControllerBase
    {
        private readonly EventoContext _context;
        private readonly IMapper _mapper;
        public EventoController(EventoContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> ObterEventos()
        {
            try
            {
                var eventos = await _context.Eventos.ToListAsync();
                List<ReadEventoDTO> readEventoDTOs = _mapper.Map<List<ReadEventoDTO>>(eventos);
                return Ok(readEventoDTOs);
            }
            catch (Exception)
            {
                return StatusCode(500, "Banco de dados falhou");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if(evento == null)
            {
                NotFound("Evento não existe no banco de dados");
            }

            ReadEventoDTO eventoRead = _mapper.Map<ReadEventoDTO>(evento);

            return Ok(eventoRead);
        }

        [HttpPost("")]
        public async Task<IActionResult> CadastrarEvento([FromBody] CreateEventoDTO eventoAdd)
        {
            Evento evento = _mapper.Map<Evento>(eventoAdd);
            await _context.Eventos.AddAsync(evento);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterEvento), new { id = evento.EventoId }, eventoAdd);
        }
    }
}

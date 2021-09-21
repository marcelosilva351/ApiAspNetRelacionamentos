using Dominio;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers.Controllers
{
 
    [ApiController]
    [Route("api/eventos")]
    public class EventoController : ControllerBase
    {
        private readonly IProagilRepository repository;

        public EventoController(IProagilRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> ObterEventos()
        {
            try
            { 
                var eventos = await repository.ObterEventosAsync(true);
                return Ok(eventos);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterEvento(int id)
        {
            var Evento = await repository.ObterEventoPorId(id, true);
            if(Evento == null)
            {
                return NotFound("Evento não encontrado");
            }
            return Ok(Evento);
        }


        [HttpPost]

         public async Task<IActionResult> AdicionarEvento([FromBody] Evento eventoAdd)
        {
            repository.Add<Evento>(eventoAdd);
            if(await repository.SaveChangesAsync())
            {
                return CreatedAtAction(nameof(ObterEvento), new {id = eventoAdd.EventoId }, eventoAdd);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> AtualizarEvento(int id, [FromBody] Evento EventoAtualizar)
        {
            var eventoRepositorio = await repository.ObterEventoPorId(id, false);
            if(eventoRepositorio == null)
            {
                return NotFound();
            }

            eventoRepositorio.DataEvento = EventoAtualizar.DataEvento;
            eventoRepositorio.Email = EventoAtualizar.Email;
            eventoRepositorio.QtdPessoas = EventoAtualizar.QtdPessoas;
            eventoRepositorio.RedeSocias = EventoAtualizar.RedeSocias;
            eventoRepositorio.Tema = EventoAtualizar.Tema;
            eventoRepositorio.Telefone = EventoAtualizar.Telefone;
            eventoRepositorio.RedeSocias = EventoAtualizar.RedeSocias;
            eventoRepositorio.Local = EventoAtualizar.Local;
            eventoRepositorio.Lotes = EventoAtualizar.Lotes;
            eventoRepositorio.Palestrantes = EventoAtualizar.Palestrantes;

            repository.Update<Evento>(eventoRepositorio);
            if (await repository.SaveChangesAsync())
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }


        }
       
        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeletarEvento(int id)
        {
            var Evento = await repository.ObterEventoPorId(id, false);
            if(Evento == null)
            {
                return NotFound();
            }
            repository.Delete<Evento>(Evento);
            if(await repository.SaveChangesAsync())
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost("AdicionarPalestrante/{id}")]
        public async Task<IActionResult> AddPalestrante(int id,[FromQuery] int idPalestrante)
        {
            var eventoPalestrante = new PalestranteEvento();
            eventoPalestrante.PalestranteId = idPalestrante;
            eventoPalestrante.EventoId = id;
            repository.Add<PalestranteEvento>(eventoPalestrante);
            repository.SaveChangesAsync();

            if(await repository.SaveChangesAsync())
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Não foi possivel adicioanr palestrante");
            }

            

            
        }
    }
}

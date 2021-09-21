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
    [Route("api/palestrantes")]
    public class PalestranteController : ControllerBase
    {
        private readonly IProagilRepository repository;

        public PalestranteController(IProagilRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]

        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var palestrantes = await repository.ObtertodosPalestrantesAsync(true);
                return Ok(palestrantes);


            }
            catch (Exception e)
            {
                return StatusCode(500, "Banco de dados falhou");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            var palestrante = await repository.ObterPalestrantePorIdAsync(id, true);
            if(palestrante == null)
            {
                return NotFound();
            }
            return Ok(palestrante);

        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Palestrante palestrante)
        {
               repository.Add<Palestrante>(palestrante);
            if (await repository.SaveChangesAsync()){
                return CreatedAtAction(nameof(Obter), new { id = palestrante.Id }, palestrante);
            }
            else
            {
                return BadRequest();
            }
        }


    }
}

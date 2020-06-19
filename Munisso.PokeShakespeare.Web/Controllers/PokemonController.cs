using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Munisso.PokeShakespeare.Models;
using Munisso.PokeShakespeare.Services;

namespace Munisso.PokeShakespeare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        public PokemonController(IPokeShakespeareService pokeShakespeareService)
        {
            PokeShakespeareService = pokeShakespeareService ?? throw new ArgumentNullException(nameof(pokeShakespeareService));
        }

        public IPokeShakespeareService PokeShakespeareService { get; }


        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }

            try
            {
                var description = await PokeShakespeareService.GetPokemonDescription(name);
                return Ok(description);
            }
            catch (ArgumentException)
            {
                // the only case we can end up here is that the pokeapi returns not found
                // ideally we could use specialized exceptions to make the detection more clear
                return NotFound();
            }
        }
    }
}

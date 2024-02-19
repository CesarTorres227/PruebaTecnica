using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Models;
using PruebaTecnica.Services;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokeApi _pokeApi;
        public PokemonController(IPokeApi pokeApi) => _pokeApi = pokeApi;

        [HttpGet]
        public async Task<PokemonTypeResultModel> GetPokemons(string name)
        {
            return await _pokeApi.Get(name);
        }
    }
}

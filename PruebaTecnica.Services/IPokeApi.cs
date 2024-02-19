using PruebaTecnica.Models;

namespace PruebaTecnica.Services
{
    public interface IPokeApi
    {
        Task<PokemonTypeResultModel> Get(string name);
    }
}

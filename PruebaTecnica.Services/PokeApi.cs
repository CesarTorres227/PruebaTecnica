using Newtonsoft.Json;
using PruebaTecnica.Models;

namespace PruebaTecnica.Services
{
    public class PokeApi : IPokeApi
    {
        private readonly HttpClient _httpClient;
        public PokeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<PokemonTypeResultModel> Get(string name)
        {
            List<string> tps = new List<string> { "Fire", "Electric" };

            List<PokemonInfoModel> ap = new List<PokemonInfoModel>();
            foreach (var t in tps)
            {
                try
                {
                    string aUrl = $"type/{t.ToLower()}/";
                    HttpResponseMessage rsp = _httpClient.GetAsync(aUrl).Result; 
                    rsp.EnsureSuccessStatusCode();
                    string aRsp = rsp.Content.ReadAsStringAsync().Result;
                    var ptRes = JsonConvert.DeserializeObject<PokemonTypeResultModel>(aRsp);
                    foreach (var p in ptRes.Pokemon)
                    {
                        string pUrl = p.Pokemon.Url;
                        HttpResponseMessage prsp = _httpClient.GetAsync(pUrl).Result;
                        prsp.EnsureSuccessStatusCode();
                        string prspRsp = prsp.Content.ReadAsStringAsync().Result;
                        var pInfo = JsonConvert.DeserializeObject<PokemonInfoModel>(prspRsp);
                        ap.Add(pInfo);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"¡Err al btr ls Pkmn dl tpo '{t}'!");
                }
            }
            return Ok(JsonConvert.SerializeObject(ap));
        }
    }
}

using Beers.Models;


namespace Beers.Services
{
    public interface IPunkApiService
    {
        Task<IEnumerable<Beer>> GetMenu();
        Task<Beer> GetBeerById(int id);
        Task<Beer> GetRandomBeer();
        Task<IEnumerable<Beer>> SearchBeers(string query);
    }
}

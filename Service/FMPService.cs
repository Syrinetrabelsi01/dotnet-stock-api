using System.Net.Http;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private readonly HttpClient _httpClient;

        public FMPService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stock?> FindStockBySymbolAsync(string symbol)
        {
            // Sample dummy logic, you should replace it with actual API call
            return await Task.FromResult(new Stock
            {
                Symbol = symbol,
                CompanyName = "Dummy Company",
                Purchase = 100,
                LastDiv = 1.2M,
                Industry = "Tech",
                MarketCap = 5000000
            });
        }
    }
}

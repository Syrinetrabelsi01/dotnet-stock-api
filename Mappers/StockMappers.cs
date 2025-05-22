using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        // Stock → StockDto (used for GET responses)
        public static StockDto ToDto(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap
            };
        }

        // StockCreateDto → Stock (used in POST)
        public static Stock ToModel(this StockCreateDto dto)
        {
            return new Stock
            {
                Symbol = dto.Symbol ?? string.Empty,
                CompanyName = dto.CompanyName ?? string.Empty,
                Purchase = dto.Purchase,
                LastDiv = dto.LastDiv,
                Industry = dto.Industry ?? string.Empty,
                MarketCap = dto.MarketCap
            };
        }

        // StockUpdateDto → Stock (used in PUT, applies changes)
        public static void UpdateFromDto(this Stock stock, StockUpdateDto dto)
        {
            if (dto.Symbol != null)
                stock.Symbol = dto.Symbol;

            if (dto.CompanyName != null)
                stock.CompanyName = dto.CompanyName;

            if (dto.Purchase.HasValue)
                stock.Purchase = dto.Purchase.Value;

            if (dto.LastDiv.HasValue)
                stock.LastDiv = dto.LastDiv.Value;

            if (dto.Industry != null)
                stock.Industry = dto.Industry;

            if (dto.MarketCap.HasValue)
                stock.MarketCap = dto.MarketCap.Value;
        }
    }
}

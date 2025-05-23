using System;
using System.Linq;
using api.Models;

namespace api.Helpers
{
    public static class StockExtensions
    {
        // This method filters stocks by symbol and company name
        public static IQueryable<Stock> Filter(this IQueryable<Stock> query, QueryObject queryObj)
        {
            if (!string.IsNullOrWhiteSpace(queryObj.Symbol))
                query = query.Where(s => s.Symbol.Contains(queryObj.Symbol));

            if (!string.IsNullOrWhiteSpace(queryObj.CompanyName))
                query = query.Where(s => s.CompanyName.Contains(queryObj.CompanyName));

            return query;
        }

        // This method sorts stocks based on the SortBy property
        public static IQueryable<Stock> Sort(this IQueryable<Stock> query, QueryObject queryObj)
        {
            if (!string.IsNullOrWhiteSpace(queryObj.SortBy))
            {
                if (queryObj.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                    query = queryObj.IsDecsending ? query.OrderByDescending(s => s.Symbol) : query.OrderBy(s => s.Symbol);
                else if (queryObj.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                    query = queryObj.IsDecsending ? query.OrderByDescending(s => s.CompanyName) : query.OrderBy(s => s.CompanyName);
            }

            return query;
        }
    }
}

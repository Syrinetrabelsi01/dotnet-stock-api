using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;

namespace api.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; } // Primary key
        public string Symbol { get; set; } = string.Empty; // Stock symbol
        public string CompanyName { get; set; } = string.Empty; // Company name   

        [Column(TypeName = "decimal(18, 2)")] // Precision and scale for decimal
        public decimal Purchase { get; set; } // Purchase price
        [Column(TypeName = "decimal(18, 2)")]// Precision and scale for decimal
        public decimal LastDiv { get; set; } // Last dividend
        public string Industry { get; set; } = string.Empty; // Industry
        public long MarketCap { get; set; } // Market capitalization
        public List<Comment> Comments { get; set; } = new List<Comment>(); // Navigation property for comments
        public bool IsDeleted { get; set; } = false; // Default to false
        public DateTime? DeletedAt { get; set; }

        internal void UpdateFromDto(StockCreateDto stockDto)
        {
            throw new NotImplementedException();
        }
        public ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
        
    }
}
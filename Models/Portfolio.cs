using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public int Id { get; set; }  

        public string Name { get; set; } = string.Empty;

        // ✅ Correct: only one declaration for each
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;

        public int StockId { get; set; }
        public Stock Stock { get; set; } = null!;

        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

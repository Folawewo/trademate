using System;
namespace trademate.Models
{
	public class Stock
	{
        public int Id { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}


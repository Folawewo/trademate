using System;
namespace trademate.Models
{
	public class Portfolio
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public List<Stock>? Stocks { get; set; }
	}
}


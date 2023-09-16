using System;
using Microsoft.EntityFrameworkCore;
using trademate.Models;

namespace trademate.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Portfolio> Portfolios { get; set; }
		public DbSet<Stock> Stocks { get; set; }
	}
}


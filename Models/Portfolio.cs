using System;
using trademate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace trademate.Models
{
	public class Portfolio
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public List<Stock>? Stocks { get; set; }
	}


public static class PortfolioEndpoints
{
	public static void MapPortfolioEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Portfolio").WithTags(nameof(Portfolio));

        group.MapGet("/", async (ApplicationDbContext db) =>
        {
            return await db.Portfolios.ToListAsync();
        })
        .WithName("GetAllPortfolios")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Portfolio>, NotFound>> (int id, ApplicationDbContext db) =>
        {
            return await db.Portfolios.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Portfolio model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPortfolioById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Portfolio portfolio, ApplicationDbContext db) =>
        {
            var affected = await db.Portfolios
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, portfolio.Id)
                  .SetProperty(m => m.Name, portfolio.Name)
                  .SetProperty(m => m.Description, portfolio.Description)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePortfolio")
        .WithOpenApi();

        group.MapPost("/", async (Portfolio portfolio, ApplicationDbContext db) =>
        {
            db.Portfolios.Add(portfolio);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Portfolio/{portfolio.Id}",portfolio);
        })
        .WithName("CreatePortfolio")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ApplicationDbContext db) =>
        {
            var affected = await db.Portfolios
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePortfolio")
        .WithOpenApi();
    }
}}


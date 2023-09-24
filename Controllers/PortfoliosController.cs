using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trademate.Data;
using trademate.Models;

namespace trademate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PortfoliosController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetPortfolios()
        {
            return await _context.Portfolios.ToListAsync();
        }

        // api/Portfolios

        [HttpGet("{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolio(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);

            if (portfolio == null)
            {
                return NotFound();
            }

            return portfolio;

        }

        [HttpPost]
        public async Task<ActionResult<Portfolio>> PostPortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPortfolio), new { id = portfolio.Id }, portfolio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortfolio(int id, Portfolio portfolio)
        {
            if (id != portfolio.Id)
            {
                return BadRequest();
            }
            _context.Entry(portfolio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolio(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if ( portfolio == null)
            {
                return NotFound();
            }
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.Id == id);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTAP.Core.Models;
using PTAP.Infrastructure;
using PTAP.Infrastructure.Data;

namespace PTAP.Web.Controllers
{
    public class KanyeController : Controller
    {
        private readonly KanyeContext _context;
        private readonly KanyeClient _kanyeClient;

        public KanyeController(KanyeContext context, KanyeClient kanye)
        {
            _context = context;
            _kanyeClient = kanye;
        }

        public async Task<IActionResult> Index()
        {
            if (_kanyeClient.Quote != null)
            {
                ViewBag.CurrentQuote = _kanyeClient.Quote.QuoteText;
                _context.Add(_kanyeClient.Quote);
                await _context.SaveChangesAsync();
            }
            else
            {
                ViewBag.CurrentQuote = "Quote could not be retrieved from source.";
            }

            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Quote == null)
            {
                return NotFound();
            }

            var quote = await _context.Quote
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuoteText")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quote);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Quote == null)
            {
                return NotFound();
            }

            var quote = await _context.Quote.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }
            return View(quote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuoteText")] Quote quote)
        {
            if (id != quote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoteExists(quote.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quote);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Quote == null)
            {
                return NotFound();
            }

            var quote = await _context.Quote
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quote == null)
            {
                return Problem("Entity set 'KanyeContext.Quote'  is null.");
            }
            var quote = await _context.Quote.FindAsync(id);
            if (quote != null)
            {
                _context.Quote.Remove(quote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuoteExists(int id)
        {
            return (_context.Quote?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

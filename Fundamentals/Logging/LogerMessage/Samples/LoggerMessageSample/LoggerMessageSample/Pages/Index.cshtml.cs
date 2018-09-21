using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using LoggerMessageSample.Internal;
using LoggerMessageSample.Data;
using Microsoft.EntityFrameworkCore;

namespace LoggerMessageSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db, ILogger<IndexModel> logger)
        {
            this._db = db;
            this._logger = logger;
        }
        public async Task OnGetAsync()
        {
            _logger.IndexPageRequested();

            Quotes = await _db.Quotes.AsNoTracking().ToListAsync();
        }

        public IList<Quote> Quotes { get; private set; }

        [BindProperty]
        public Quote Quote { get; set; }
        public async Task<IActionResult> OnPostAddQuoteAsync()
        {
            _db.Quotes.Add(Quote);
            await _db.SaveChangesAsync();

            _logger.QuoteAdded(Quote.Text);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteQuoteAsync(int id)
        {
            var quote = await _db.Quotes.FindAsync(id);

            try
            {
                _db.Quotes.Remove(quote);
                await _db.SaveChangesAsync();

                _logger.QuoteDeleted(quote.Text, id);
            }
            catch (ArgumentNullException ex)
            {
                _logger.QuoteDeleteFailed(id, ex);
            }

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostDeleteAllQuotesAsync()
        {
            var quoteCount = await _db.Quotes.CountAsync();

            using (_logger.AllQuotesDeletedScope(quoteCount))
            {
                foreach (Quote quote in _db.Quotes)
                {
                    _db.Quotes.Remove(quote);

                    _logger.QuoteDeleted(quote.Text, quote.Id);
                }
                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}

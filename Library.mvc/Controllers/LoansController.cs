using Library.domain.Models;
using Library.mvc.Data;
using Library.MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Controllers
{
    public class LoansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (loan == null)
                return NotFound();

            return View(loan);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            var availableBooks = _context.Books
                .Where(b => !_context.Loans.Any(l => l.BookId == b.Id && l.ReturnedDate == null))
                .ToList();

            ViewData["BookId"] = new SelectList(availableBooks, "Id", "Title");
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FullName");

            return View();
        }

        // POST: Loans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,MemberId,DueDate")] Loan loan)
        {
            var isLoaned = _context.Loans
                .Any(l => l.BookId == loan.BookId && l.ReturnedDate == null);

            if (isLoaned)
            {
                ModelState.AddModelError("", "This book is already on loan.");
            }

            if (ModelState.IsValid)
            {
                loan.LoanDate = DateTime.Now;
                loan.ReturnedDate = null;

                var book = await _context.Books.FindAsync(loan.BookId);
                if (book != null)
                {
                    book.IsAvailable = false;
                }

                _context.Loans.Add(loan);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var availableBooks = _context.Books
                .Where(b => !_context.Loans.Any(l => l.BookId == b.Id && l.ReturnedDate == null) || b.Id == loan.BookId)
                .ToList();

            ViewData["BookId"] = new SelectList(availableBooks, "Id", "Title", loan.BookId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FullName", loan.MemberId);

            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
                return NotFound();

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", loan.BookId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FullName", loan.MemberId);

            return View(loan);
        }

        // POST: Loans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,MemberId,LoanDate,DueDate,ReturnedDate")] Loan loan)
        {
            if (id != loan.Id)
                return NotFound();

            var activeLoanForSameBook = _context.Loans.Any(l =>
                l.BookId == loan.BookId &&
                l.ReturnedDate == null &&
                l.Id != loan.Id);

            if (activeLoanForSameBook)
            {
                ModelState.AddModelError("", "This book is already on loan.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);

                    // 🔥 Atualiza disponibilidade automaticamente
                    var book = await _context.Books.FindAsync(loan.BookId);
                    if (book != null)
                    {
                        book.IsAvailable = loan.ReturnedDate != null;
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", loan.BookId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FullName", loan.MemberId);

            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (loan == null)
                return NotFound();

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan != null)
            {
                // 🔥 devolve o livro ao deletar loan
                var book = await _context.Books.FindAsync(loan.BookId);
                if (book != null)
                {
                    book.IsAvailable = true;
                }

                _context.Loans.Remove(loan);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Loans/Return/5
        public async Task<IActionResult> Return(int id)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
                return NotFound();

            if (loan.ReturnedDate == null)
            {
                loan.ReturnedDate = DateTime.Now;

                var book = await _context.Books.FindAsync(loan.BookId);
                if (book != null)
                {
                    book.IsAvailable = true;
                }

                _context.Update(loan);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.Id == id);
        }
    }
}
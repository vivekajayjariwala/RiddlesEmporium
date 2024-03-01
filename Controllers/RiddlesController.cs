using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RiddlesWebApp.Data;
using RiddlesWebApp.Models;

namespace RiddlesWebApp.Controllers
{
    public class RiddlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiddlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Riddles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Riddle.ToListAsync());
        }

        // GET: Riddles/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View("ShowSearchForm");
        }

        // POST: Riddles/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Riddle.Where( j => j.RiddleQuestion.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Riddles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riddle = await _context.Riddle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riddle == null)
            {
                return NotFound();
            }

            return View(riddle);
        }

        // GET: Riddles/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Riddles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RiddleQuestion,RiddleAnswer")] Riddle riddle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riddle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(riddle);
        }

        // GET: Riddles/Edit/5

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riddle = await _context.Riddle.FindAsync(id);
            if (riddle == null)
            {
                return NotFound();
            }
            return View(riddle);
        }

        // POST: Riddles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RiddleQuestion,RiddleAnswer")] Riddle riddle)
        {
            if (id != riddle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riddle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiddleExists(riddle.Id))
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
            return View(riddle);
        }

        // GET: Riddles/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riddle = await _context.Riddle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riddle == null)
            {
                return NotFound();
            }

            return View(riddle);
        }

        // POST: Riddles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var riddle = await _context.Riddle.FindAsync(id);
            if (riddle != null)
            {
                _context.Riddle.Remove(riddle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiddleExists(int id)
        {
            return _context.Riddle.Any(e => e.Id == id);
        }
    }
}

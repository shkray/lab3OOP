﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba3.Data;
using Laba3.Models;

namespace Laba3.Controllers
{
    public class ProfessionsController : Controller
    {
        private readonly AppDbContext _context;

        public ProfessionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Professions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profession.ToListAsync());
        }

        // GET: Professions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profession = await _context.Profession
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profession == null)
            {
                return NotFound();
            }

            return View(profession);
        }

        // GET: Professions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Profession profession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profession);
        }

        // GET: Professions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profession = await _context.Profession.FindAsync(id);
            if (profession == null)
            {
                return NotFound();
            }
            return View(profession);
        }

        // POST: Professions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Profession profession)
        {
            if (id != profession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessionExists(profession.Id))
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
            return View(profession);
        }

        // GET: Professions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profession = await _context.Profession
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profession == null)
            {
                return NotFound();
            }

            return View(profession);
        }

        // POST: Professions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profession = await _context.Profession.FindAsync(id);
            if (profession != null)
            {
                _context.Profession.Remove(profession);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessionExists(int id)
        {
            return _context.Profession.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using BUA_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BUA_project.Controllers
{
    public class DriverController : Controller
    {
        private readonly Entity _context = new Entity();


        private async Task LoadUsers()
        {
            ViewBag.Users = await _context.Users
                .ToListAsync();
        }

        // GET: Driver
        public async Task<IActionResult> Index()
        {
            var drivers = await _context.Drivers
            .Include(d => d.User)
            .ToListAsync();

            return View(drivers);
        }

        // GET: Driver/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.DriverId == id);

            if (driver == null)
                return NotFound();

            return View(driver);
        }

        // GET: Driver/Create
        public async Task<IActionResult> Create()
        {
            await LoadUsers();

            return View();
        }

        // POST: Driver/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                await LoadUsers();

                return View(driver);
            }

            _context.Drivers.Add(driver);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Driver/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.DriverId == id);

            if (driver == null)
                return NotFound();

            return View(driver);
        }

        // POST: Driver/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Driver driver)
        {
            if (id != driver.DriverId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(driver);

            try
            {
                _context.Drivers.Update(driver);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(driver.DriverId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Driver/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.DriverId == id);

            if (driver == null)
                return NotFound();

            return View(driver);
        }

        // POST: Driver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.DriverId == id);

            if (driver == null)
                return NotFound();

            _context.Drivers.Remove(driver);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers
                .Any(d => d.DriverId == id);
        }
    }
    
}

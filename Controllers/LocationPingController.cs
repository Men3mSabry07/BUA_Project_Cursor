using BUA_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BUA_project.Controllers
{
    public class LocationPingController : Controller
    {
        private readonly Entity _context = new Entity();


        // GET: LocationPing
        public async Task<IActionResult> Index()
        {
            var locationPings = await _context.LocationPings
                .Include(l => l.Trip)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();

            return View(locationPings);
        }

        // GET: LocationPing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var locationPing = await _context.LocationPings
                .Include(l => l.Trip)
                .FirstOrDefaultAsync(
                    l => l.LocationPingId == id);

            if (locationPing == null)
                return NotFound();

            return View(locationPing);
        }

        // GET: LocationPing/Create
        public async Task<IActionResult> Create()
        {
            await LoadTrips();

            return View();
        }

        // POST: LocationPing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            LocationPing locationPing)
        {
            if (!ModelState.IsValid)
            {
                await LoadTrips();

                return View(locationPing);
            }

            _context.LocationPings.Add(locationPing);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: LocationPing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var locationPing = await _context.LocationPings
                .FirstOrDefaultAsync(
                    l => l.LocationPingId == id);

            if (locationPing == null)
                return NotFound();

            await LoadTrips();

            return View(locationPing);
        }

        // POST: LocationPing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            LocationPing locationPing)
        {
            if (id != locationPing.LocationPingId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadTrips();

                return View(locationPing);
            }

            try
            {
                _context.LocationPings.Update(locationPing);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationPingExists(
                    locationPing.LocationPingId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: LocationPing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var locationPing = await _context.LocationPings
                .Include(l => l.Trip)
                .FirstOrDefaultAsync(
                    l => l.LocationPingId == id);

            if (locationPing == null)
                return NotFound();

            return View(locationPing);
        }

        // POST: LocationPing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locationPing = await _context.LocationPings
                .FirstOrDefaultAsync(
                    l => l.LocationPingId == id);

            if (locationPing == null)
                return NotFound();

            _context.LocationPings.Remove(locationPing);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadTrips()
        {
            ViewBag.Trips = await _context.Trips
                .Where(t => t.Status == "InProgress")
                .ToListAsync();
        }

        private bool LocationPingExists(int id)
        {
            return _context.LocationPings
                .Any(l => l.LocationPingId == id);
        }
    }
}
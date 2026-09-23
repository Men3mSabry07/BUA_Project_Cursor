using BUA_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BUA_project.Controllers
{
    public class RouteEstimateController : Controller
    {
        private readonly Entity _context = new Entity();

        // GET: RouteEstimate
        public async Task<IActionResult> Index()
        {
            var routeEstimates = await _context.RouteEstimates
                .Include(r => r.Trip)
                .OrderByDescending(r => r.RouteEstimateId)
                .ToListAsync();

            return View(routeEstimates);
        }

        // GET: RouteEstimate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var routeEstimate = await _context.RouteEstimates
                .Include(r => r.Trip)
                .FirstOrDefaultAsync(
                    r => r.RouteEstimateId == id);

            if (routeEstimate == null)
                return NotFound();

            return View(routeEstimate);
        }

        // GET: RouteEstimate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RouteEstimate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            RouteEstimate routeEstimate)
        {
            if (!ModelState.IsValid)
            {
                return View(routeEstimate);
            }

            _context.RouteEstimates.Add(routeEstimate);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: RouteEstimate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var routeEstimate = await _context.RouteEstimates
                .FirstOrDefaultAsync(
                    r => r.RouteEstimateId == id);

            if (routeEstimate == null)
                return NotFound();

            return View(routeEstimate);
        }

        // POST: RouteEstimate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            RouteEstimate routeEstimate)
        {
            if (id != routeEstimate.RouteEstimateId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View(routeEstimate);
            }

            try
            {
                _context.RouteEstimates.Update(routeEstimate);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteEstimateExists(
                    routeEstimate.RouteEstimateId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: RouteEstimate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var routeEstimate = await _context.RouteEstimates
                .Include(r => r.Trip)
                .FirstOrDefaultAsync(
                    r => r.RouteEstimateId == id);

            if (routeEstimate == null)
                return NotFound();

            return View(routeEstimate);
        }

        // POST: RouteEstimate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routeEstimate =
                await _context.RouteEstimates
                    .FirstOrDefaultAsync(
                        r => r.RouteEstimateId == id);

            if (routeEstimate == null)
                return NotFound();

            _context.RouteEstimates.Remove(routeEstimate);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool RouteEstimateExists(int id)
        {
            return _context.RouteEstimates
                .Any(r => r.RouteEstimateId == id);
        }
    }
}
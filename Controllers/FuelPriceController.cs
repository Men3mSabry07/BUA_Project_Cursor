using BUA_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BUA_project.Controllers
{
    public class FuelPriceController : Controller
    {
        private readonly Entity _context;

        public FuelPriceController(Entity context)
        {
            _context = context;
        }

        // GET: FuelPrice
        public async Task<IActionResult> Index()
        {
            var fuelPrice = await _context.FuelPrices
                .OrderByDescending(f => f.UpdatedAt)
                .FirstOrDefaultAsync();

            return View(fuelPrice);
        }

        // GET: FuelPrice/Edit
        public async Task<IActionResult> Edit()
        {
            var fuelPrice = await _context.FuelPrices
                .OrderByDescending(f => f.UpdatedAt)
                .FirstOrDefaultAsync();

            if (fuelPrice == null)
            {
                fuelPrice = new FuelPrice
                {
                    PricePerLiter = 0,
                    UpdatedAt = DateTime.Now
                };
            }

            return View(fuelPrice);
        }

        // POST: FuelPrice/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            FuelPrice model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var currentPrice = await _context.FuelPrices
                .OrderByDescending(f => f.UpdatedAt)
                .FirstOrDefaultAsync();

            if (currentPrice == null)
            {
                currentPrice = new FuelPrice
                {
                    PricePerLiter = model.PricePerLiter,
                    UpdatedAt = DateTime.Now
                };

                _context.FuelPrices.Add(currentPrice);
            }
            else
            {
                currentPrice.PricePerLiter =
                    model.PricePerLiter;

                currentPrice.UpdatedAt =
                    DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
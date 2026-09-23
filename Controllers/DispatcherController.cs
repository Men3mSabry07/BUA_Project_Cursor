using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BUA_project.Controllers
{
    using BUA_project.Models;
    using Microsoft.AspNetCore.Mvc;
        public class DispatcherController : Controller
        {
        private readonly Entity _context = new Entity();



        // GET: Dispatcher
        // عرض الطلبات التي تحتاج إلى معالجة
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Vehicle)
                .Include(r => r.User)
                .Include(r => r.Driver)
                .Where(r =>
                    r.Status == "Pending" ||
                    (r.Status == "Approved" && r.DriverId == null))
                .ToListAsync();

            return View(reservations);
        }


        // GET: Dispatcher/Details/5
        public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                    return NotFound();

                var reservation = await _context.Reservations
                    .Include(r => r.Vehicle)
                    .Include(r => r.User)
                    .Include(r => r.Driver)
                    .FirstOrDefaultAsync(
                        r => r.ReservationId == id);

                if (reservation == null)
                    return NotFound();

                return View(reservation);
            }


            // GET: Dispatcher/Approve/5
            public async Task<IActionResult> Approve(int? id)
            {
                if (id == null)
                    return NotFound();

                var reservation = await _context.Reservations
                    .Include(r => r.Vehicle)
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(
                        r => r.ReservationId == id);

                if (reservation == null)
                    return NotFound();

                return View(reservation);
            }


            // POST: Dispatcher/Approve
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Approve(int id)
            {
                var reservation = await _context.Reservations
                    .FirstOrDefaultAsync(
                        r => r.ReservationId == id);

                if (reservation == null)
                    return NotFound();

                reservation.Status = "Approved";

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            // GET: Dispatcher/Reject/5
            public async Task<IActionResult> Reject(int? id)
            {
                if (id == null)
                    return NotFound();

                var reservation = await _context.Reservations
                    .Include(r => r.Vehicle)
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(
                        r => r.ReservationId == id);

                if (reservation == null)
                    return NotFound();

                return View(reservation);
            }


            // POST: Dispatcher/Reject
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Reject(int id)
            {
                var reservation = await _context.Reservations
                    .FirstOrDefaultAsync(
                        r => r.ReservationId == id);

                if (reservation == null)
                    return NotFound();

                reservation.Status = "Rejected";

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            // GET: Dispatcher/AssignDriver/5
            public async Task<IActionResult> AssignDriver(int? id)
            {
                if (id == null)
                    return NotFound();

                var reservation = await _context.Reservations
                    .Include(r => r.Vehicle)
                    .Include(r => r.User)
                    .Include(r => r.Driver)
                    .FirstOrDefaultAsync(
                        r => r.ReservationId == id);

                if (reservation == null)
                    return NotFound();

                if (reservation.Status != "Approved")
                    return BadRequest(
                        "Reservation must be approved first.");

                ViewBag.Drivers = await _context.Drivers
                    .ToListAsync();

                return View(reservation);
            }


            // POST: Dispatcher/AssignDriver
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AssignDriver(
                int id,
                int driverId)
            {
                var reservation = await _context.Reservations
                    .FirstOrDefaultAsync(
                        r => r.ReservationId == id);

                if (reservation == null)
                    return NotFound();

                if (reservation.Status != "Approved")
                    return BadRequest(
                        "Reservation must be approved first.");

                var driver = await _context.Drivers
                    .FirstOrDefaultAsync(
                        d => d.DriverId == driverId);

                if (driver == null)
                    return NotFound();

                reservation.DriverId = driverId;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
    
}

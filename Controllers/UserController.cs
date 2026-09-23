using BUA_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BUA_project.Controllers
{
    public class UserController : Controller
    {
        private readonly Entity _context = new Entity();

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .OrderBy(u => u.Name)
                .ToListAsync();

            return View(users);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.Users
                .Include(u => u.Reservations)
                .Include(u => u.Driver)
                .FirstOrDefaultAsync(
                    u => u.UserId == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            // Check if email already exists
            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == user.Email);

            if (emailExists)
            {
                ModelState.AddModelError(
                    "Email",
                    "This email is already registered.");

                return View(user);
            }

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.UserId == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            User user)
        {
            if (id != user.UserId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            // Check email belongs to another user
            var emailExists = await _context.Users
                .AnyAsync(u =>
                    u.Email == user.Email &&
                    u.UserId != user.UserId);

            if (emailExists)
            {
                ModelState.AddModelError(
                    "Email",
                    "This email is already registered.");

                return View(user);
            }

            try
            {
                _context.Users.Update(user);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.UserId == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.UserId == id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users
                .Any(u => u.UserId == id);
        }
    }
}
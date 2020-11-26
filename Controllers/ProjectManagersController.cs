using Company_Project_Expenses_MVC.Data;
using Company_Project_Expenses_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Project_Expenses_MVC.Controllers
{
    public class ProjectManagersController : Controller
    {
        private readonly Company_Project_Expenses_DbContext _context;

        public ProjectManagersController(Company_Project_Expenses_DbContext context)
        {
            _context = context;
        }

        // GET: ProjectManagers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectManager.ToListAsync());
        }

        // GET: ProjectManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectManager = await _context.ProjectManager
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectManager == null)
            {
                return NotFound();
            }

            return View(projectManager);
        }
        [Authorize]
        // GET: ProjectManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectManagerName,Email")] ProjectManager projectManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectManager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectManager);
        }
        [Authorize]
        // GET: ProjectManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectManager = await _context.ProjectManager.FindAsync(id);
            if (projectManager == null)
            {
                return NotFound();
            }
            return View(projectManager);
        }

        // POST: ProjectManagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectManagerName,Email")] ProjectManager projectManager)
        {
            if (id != projectManager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectManagerExists(projectManager.Id))
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
            return View(projectManager);
        }
        [Authorize]
        // GET: ProjectManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectManager = await _context.ProjectManager
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectManager == null)
            {
                return NotFound();
            }

            return View(projectManager);
        }

        // POST: ProjectManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectManager = await _context.ProjectManager.FindAsync(id);
            _context.ProjectManager.Remove(projectManager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectManagerExists(int id)
        {
            return _context.ProjectManager.Any(e => e.Id == id);
        }
    }
}

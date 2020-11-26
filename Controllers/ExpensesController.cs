using Company_Project_Expenses_MVC.Data;
using Company_Project_Expenses_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Project_Expenses_MVC.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly Company_Project_Expenses_DbContext _context;

        public ExpensesController(Company_Project_Expenses_DbContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var company_Project_Expenses_DbContext = _context.Expense.Include(e => e.Company).Include(e => e.Project).Include(e => e.ProjectManager);
            return View(await company_Project_Expenses_DbContext.ToListAsync());
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .Include(e => e.Company)
                .Include(e => e.Project)
                .Include(e => e.ProjectManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Id");
            ViewData["ProjectManagerId"] = new SelectList(_context.Set<ProjectManager>(), "Id", "Id");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,ProjectId,ProjectManagerId,ExpenseDescription,SpentAmount")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", expense.CompanyId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Id", expense.ProjectId);
            ViewData["ProjectManagerId"] = new SelectList(_context.Set<ProjectManager>(), "Id", "Id", expense.ProjectManagerId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", expense.CompanyId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Id", expense.ProjectId);
            ViewData["ProjectManagerId"] = new SelectList(_context.Set<ProjectManager>(), "Id", "Id", expense.ProjectManagerId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyId,ProjectId,ProjectManagerId,ExpenseDescription,SpentAmount")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", expense.CompanyId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Id", expense.ProjectId);
            ViewData["ProjectManagerId"] = new SelectList(_context.Set<ProjectManager>(), "Id", "Id", expense.ProjectManagerId);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .Include(e => e.Company)
                .Include(e => e.Project)
                .Include(e => e.ProjectManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expense.FindAsync(id);
            _context.Expense.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expense.Any(e => e.Id == id);
        }
    }
}

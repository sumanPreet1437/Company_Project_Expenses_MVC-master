using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Company_Project_Expenses_MVC.Models;

namespace Company_Project_Expenses_MVC.Data
{
    public class Company_Project_Expenses_DbContext : DbContext
    {
        public Company_Project_Expenses_DbContext (DbContextOptions<Company_Project_Expenses_DbContext> options)
            : base(options)
        {
        }

        public DbSet<Company_Project_Expenses_MVC.Models.Company> Company { get; set; }

        public DbSet<Company_Project_Expenses_MVC.Models.Expense> Expense { get; set; }

        public DbSet<Company_Project_Expenses_MVC.Models.Project> Project { get; set; }

        public DbSet<Company_Project_Expenses_MVC.Models.ProjectManager> ProjectManager { get; set; }
    }
}

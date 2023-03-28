using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer.Data
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\;Database=HomeLoan;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            optionsBuilder.LogTo(Console.WriteLine);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Advisor> Advisors { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<LoanRequirements> LoanRequirements { get; set; }
        public DbSet<PersonalIncome> PersonalIncomes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Promotions> Promotions { get; set;}
        public DbSet<Collateral> Collaterals { get; set; }

    }
}


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_task.Models;

namespace Test_task
{
	public class ApplicationContext: DbContext
	{
		public DbSet<Manager> Managers { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
        public ApplicationContext()
        {
			Database.EnsureCreated();
        }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TaskDatabase;Trusted_Connection=True;");
		}
	}
}

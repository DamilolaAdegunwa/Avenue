using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Avenue.Library.Persistence
{
	internal class AnimalContext : DbContext
	{
		public AnimalContext() { }
		public DbSet<Animal> Animals => Set<Animal>();
		public DbSet<Pet> Pets => Set<Pet>();
		public DbSet<FarmAnimal> FarmAnimals => Set<FarmAnimal>();
		public DbSet<Cat> Cats => Set<Cat>();
		public DbSet<Dog> Dogs => Set<Dog>();
		public DbSet<Human> Humans => Set<Human>();
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Animal>().UseTphMappingStrategy();
			modelBuilder.Entity<Pet>();
			modelBuilder.Entity<Cat>();
			modelBuilder.Entity<Dog>();
			modelBuilder.Entity<FarmAnimal>();
			modelBuilder.Entity<Human>();
		}
	}
}

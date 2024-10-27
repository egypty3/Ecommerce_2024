using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
	public class EcommerceContext : DbContext
	{
		public EcommerceContext(DbContextOptions<EcommerceContext> options)
			: base(options)
		{

		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<ProductBrand> ProductBrands { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasOne(p => p.ProductType)
				.WithMany()
				.HasForeignKey(p => p.ProductTypeId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Product>()
				.HasOne(p => p.ProductBrand)
				.WithMany()
				.HasForeignKey(p => p.ProductBrandId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}

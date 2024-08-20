using System.Linq.Expressions;
using DotNetService.Models;
using DotNetService.Seeders;
using Microsoft.EntityFrameworkCore;

namespace DotNetService.Infrastructure.Databases
{
    public partial class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            GenerateUuid<Product>(modelBuilder, "Id");

            new ProductSeeder(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is Base
                    && (e.State == EntityState.Added || e.State == EntityState.Modified)
                );

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    ((Base)entityEntry.Entity).UpdatedAt = DateTime.Now;
                }

                if (entityEntry.State == EntityState.Added)
                {
                    ((Base)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        /*=================================== Service Support ===========================================*/

        private void GenerateUuid<T>(ModelBuilder modelBuilder, string column)
            where T : class
        {
            modelBuilder.Entity<T>().HasIndex(CreateExpression<T>(column));

            modelBuilder
                .Entity<T>()
                .Property(CreateExpression<T>(column))
                .HasDefaultValueSql("NEWID()");
        }

        private void SetDefaultValue<T>(ModelBuilder modelBuilder, string column, dynamic value)
            where T : class
        {
            modelBuilder
                .Entity<T>()
                .Property(CreateExpression<T>(column))
                .HasDefaultValue((object)value);
        }

        private void SetUniqueColumn<T>(ModelBuilder modelBuilder, string column)
            where T : class
        {
            modelBuilder.Entity<T>().HasIndex(CreateExpression<T>(column)).IsUnique();
        }

        private void SoftDelete<T>(ModelBuilder modelBuilder)
            where T : class
        {
            modelBuilder
                .Entity<T>()
                .HasQueryFilter(u => EF.Property<DateTime?>(u, "DeletedAt") == null);
        }

        private static Expression<Func<T, object>> CreateExpression<T>(string uuid)
            where T : class
        {
            var type = typeof(T);
            var property = type.GetProperty(uuid);
            var parameter = Expression.Parameter(type);
            var access = Expression.Property(parameter, property);
            var convert = Expression.Convert(access, typeof(object));
            var function = Expression.Lambda<Func<T, object>>(convert, parameter);

            return function;
        }
    }
}

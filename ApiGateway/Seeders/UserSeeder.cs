using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace DotNetService.Seeders
{
    public class UserSeeder
    {
        public UserSeeder(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Models.User>()
                .HasData(
                    new Models.User
                    {
                        Id = Guid.NewGuid(),
                        Name = "Admin",
                        Email = "admin@admin.com",
                        Password = BC.HashPassword("Admin123!"),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    }
                );
        }
    }
}

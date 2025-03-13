using ClientesAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPI.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    Id = 1,
                    Nome = "João Silva",
                    Email = "joaosilva@dominio.com",
                    Idade = 30
                },

                new Cliente
                {
                    Id = 2,
                    Nome = "Maria Araujo",
                    Email = "mariaaraujo@dominio.com",
                    Idade = 25
                }
             );
            // Define a chave primária composta para IdentityUserLogin<string>
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(iul => new { iul.LoginProvider, iul.ProviderKey });

            // Define a chave primária composta para IdentityUserRole<string>
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(iur => new { iur.UserId, iur.RoleId });

            // Define a chave primária composta para IdentityUserToken<string>
            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(iut => new { iut.UserId, iut.LoginProvider, iut.Name });

        }
    }
}

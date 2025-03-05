using ClientesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPI.Context
{
    public class AppDbContext : DbContext
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
        }
    }
}

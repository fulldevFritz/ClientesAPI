using ClientesAPI.Context;
using ClientesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesAPI.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            try
            {
                return await _context.Clientes.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Cliente>> GetClientesByNomeAsync(string nome)
        {
            IEnumerable<Cliente> clientes;
            if (!string.IsNullOrWhiteSpace(nome))
            {
                clientes = await _context.Clientes.Where(c => c.Nome.Contains(nome)).ToListAsync();
            }
            else
            {
                clientes = await GetClientesAsync();
            }
            return clientes;
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            return cliente;
        }
        public async Task CreateClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            var existingEntity = await _context.Clientes.FindAsync(cliente.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}

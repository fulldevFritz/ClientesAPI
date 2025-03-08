using ClientesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientesAPI.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task<IEnumerable<Cliente>> GetClientesByNomeAsync(string nome);
        Task<Cliente> GetClienteAsync(int id);
        Task CreateClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(Cliente cliente);
    }
}

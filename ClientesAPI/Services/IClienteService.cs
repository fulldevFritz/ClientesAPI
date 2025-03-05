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
        Task<Cliente> CreateClienteAsync(Cliente cliente);
        Task<Cliente> UpdateClienteAsync(int id, Cliente cliente);
        Task<Cliente> DeleteClienteAsync(int id);
    }
}

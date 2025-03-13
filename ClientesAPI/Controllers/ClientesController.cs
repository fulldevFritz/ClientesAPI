using System;
using ClientesAPI.Models;
using ClientesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ClientesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                var clientes = await _clienteService.GetClientesAsync();
                if (clientes == null)
                {
                    return NotFound();
                }
                return Ok(clientes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter os clientes");
            }
        }

        [HttpGet("ClientesPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Cliente>>> GetClientesByNome([FromQuery] string nome)
        {
            try
            {
                var clientes = await _clienteService.GetClientesByNomeAsync(nome);
                if (!clientes.Any())
                {
                    return NotFound($"Não existem clientes com o nome {nome}");
                }
                return Ok(clientes);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter os clientes");
            }
        }
        
        [HttpGet("{id:int}", Name="GetCliente")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteAsync(id);
                if (cliente == null)
                {
                    return NotFound($"Não existe cliente com o id {id}");
                }
                return Ok(cliente);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter o cliente");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null)
                {
                    return BadRequest();
                }
                await _clienteService.CreateClienteAsync(cliente);
                return CreatedAtRoute(nameof(GetCliente), new { id = cliente.Id }, cliente);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar o cliente");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if (id != cliente.Id)
                {
                    return BadRequest("Id do cliente não corresponde ao id da requisição");
                }
                var clienteToUpdate = await _clienteService.GetClienteAsync(id);
                if (clienteToUpdate == null)
                {
                    return NotFound($"Não existe cliente com o id {id}");
                }
                await _clienteService.UpdateClienteAsync(cliente);
                return Ok($"Cliente com o id {id} foi atualizado com sucesso");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar o cliente");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            try
            {
                var clienteToDelete = await _clienteService.GetClienteAsync(id);
                if (clienteToDelete == null)
                {
                    return NotFound($"Não existe cliente com o id {id}");
                }
                await _clienteService.DeleteClienteAsync(clienteToDelete);
                return Ok($"Cliente com o id {id} foi deletado com sucesso");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar o cliente");
            }
        }
    }
}


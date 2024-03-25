using Microsoft.EntityFrameworkCore;
using BackEndApi.Models;
using BackEndApi.Services.Interfaces;

namespace BackEndApi.Services.Implementacion
{
    public class ClienteService : IClienteService
    {
        private DbclientesContext _dbContext;

        public ClienteService(DbclientesContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Cliente>> GetAll()
        {
            try {

                List<Cliente> listaClientes = new List<Cliente>();
                listaClientes = await _dbContext.Clientes.ToListAsync();
                return listaClientes;

            }catch(Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<Cliente> Get(int id)
        {
            try { 
                Cliente? cliente = new Cliente();

                cliente = await _dbContext.Clientes.Where(x => x.Id == id).FirstOrDefaultAsync();
                return cliente;

            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cliente> Insert(Cliente cliente)
        {
            try
            {
                _dbContext.Clientes.Add(cliente);
                await _dbContext.SaveChangesAsync();
                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Cliente>> Search(string nombre)
        {
            try
            {
                List<Cliente> listaClientes = new List<Cliente>();
                listaClientes = await _dbContext.Clientes.Where(x => x.Nombres.Contains(nombre)).ToListAsync();
                return listaClientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Cliente cliente)
        {
            try
            {
                _dbContext.Clientes.Update(cliente);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Cliente cliente)
        {
            try
            {
                _dbContext.Clientes.Remove(cliente);
                await _dbContext.SaveChangesAsync();
                return true;    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using BackEndApi.Models;

namespace BackEndApi.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAll();
        Task<Cliente> Get(int id);
        Task<List<Cliente>> Search(string nombre);
        Task<Cliente> Insert(Cliente cliente);
        Task<bool> Update(Cliente cliente);
        Task<bool> Delete(Cliente cliente);

    }
}

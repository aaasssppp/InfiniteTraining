using Assignment1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment1.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task CreateAsync(Contact contact);
        Task DeleteAsync(long id);
    }
}

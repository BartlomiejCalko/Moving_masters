using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_Web.Repository.IRepostitory
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(string url, int Id);
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<bool> CreateAsync(string url, T objectToCreate);
        Task<bool> UpdateAsync(string url, T objectToUpdate);
        Task<bool> DeleteAsync(string url, int Id);
    }
}

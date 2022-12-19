using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagement.Data.Abstract
{
    public interface IRepository<T> where T : class
    {
        public Task<IQueryable<T>> Get();
        public Task<T> GetById(int id);
        public Task Add(T entity);
        public Task Remove(int id);
        public Task Update(int id, T entity);

    }
}

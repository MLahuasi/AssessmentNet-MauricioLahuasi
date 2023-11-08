using Queue.Web.Models.Entity;

namespace Queue.Web.Services
{
    public interface IService
    {
        Task<List<CustomerQueue>?> Get();
        Task<bool> Add(CustomerQueue queue);
        Task<bool> Delete(int id);
    }
}

using Queue.Web.Models.Entity;

namespace Queue.Web.Transacctions.Command
{
    public interface ICommand
    {
        Task<Customer?> Execute();
    }
}

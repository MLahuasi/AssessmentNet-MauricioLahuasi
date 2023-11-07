using Queue.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Transacction.Command
{
    public interface ICommand
    {
        Task<Customer?> Execute();
    }
}

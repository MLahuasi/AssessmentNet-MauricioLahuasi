using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Entity;
using Queue.Transacction;

namespace Queue.Transacction.Command
{
    public class CustomerCommand : ICommand
    {

        private CustomerTransacctionInfo _customerTransacctionInfo;
        private ClientAdvisor _clientAdvisor;

        public CustomerCommand(CustomerTransacctionInfo customerTransacctionInfo, ClientAdvisor clientAdvisor)
        {
            _customerTransacctionInfo = customerTransacctionInfo;
            _clientAdvisor = clientAdvisor;
        }

        public async Task<Customer?> Execute()
        {
            if (_customerTransacctionInfo.CurrentCustomer != null)
            {
                _customerTransacctionInfo.Success = true;
                var result = await _clientAdvisor.AddCustomer(_customerTransacctionInfo.CurrentCustomer);
                return result;
            }

            return null;
        }

        public override string ToString() => _customerTransacctionInfo.ToString();
    }
}

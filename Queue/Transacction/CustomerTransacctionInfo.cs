using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Entity;

namespace Queue.Transacction
{
    public class CustomerTransacctionInfo
    {
        public CustomerTransacctionInfo(
            string customerAction,
            Customer customer
        )
        {
            CustomerAction = customerAction;
            CurrentCustomer = customer;
        }

        public string CustomerAction { get; set; }

        public bool Success { get; set; }

        public Customer CurrentCustomer { get; set; }


        public bool TransactionState()
        {
            return Success;
        }

        public override string ToString()
        {
            //return $" Acción: {CustomerAction}  Customer: [{CurrentCustomer.Id}, {CurrentCustomer.Name}, {CurrentCustomer.Ci}]  Queue: [{CurrentCustomer.Queue.Id}, {CurrentCustomer.Queue.Name}, {CurrentCustomer.Queue.Duration}]   Status: {Success}";
            return $" Acción: {CustomerAction}  Customer: [ {CurrentCustomer.Name}, {CurrentCustomer.Ci}]   Status: {Success}";
        }
    }
}

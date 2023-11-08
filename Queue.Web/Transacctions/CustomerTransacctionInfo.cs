using Queue.Web.Models.Entity;

namespace Queue.Web.Transacctions
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
            return $" Acción: {CustomerAction}  Customer: [ {CurrentCustomer.Name}, {CurrentCustomer.Ci}]   Status: {Success}";
        }
    }
}

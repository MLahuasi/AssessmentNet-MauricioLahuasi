// See https://aka.ms/new-console-template for more information
using Queue;
using Queue.Entity;
using Queue.Transacction;
using Queue.Transacction.Command;
using Queue.Transacction.Invoker;




var executeTransacction = new ExecuteTransactions();
var queue = new ClientAdvisor();
queue.ChargeCustomers.Wait();
int idNewCustomer = queue.GetMainQueue().Count();

idNewCustomer++;
Customer customer1 = new Customer(idNewCustomer.ToString(), "Mauricio");
idNewCustomer++;
Customer customer2 = new Customer(idNewCustomer.ToString(), "Graciela");
idNewCustomer++;
Customer customer3 = new Customer(idNewCustomer.ToString(), "Daniel");
idNewCustomer++;
Customer customer4 = new Customer(idNewCustomer.ToString(), "José");
idNewCustomer++;
Customer customer5 = new Customer(idNewCustomer.ToString(), "Rosa");
idNewCustomer++;
Customer customer6 = new Customer(idNewCustomer.ToString(), "Paúl");
idNewCustomer++;
Customer customer7 = new Customer(idNewCustomer.ToString(), "Angel");
idNewCustomer++;
Customer customer8 = new Customer(idNewCustomer.ToString(), "Maria");


var transacction1 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer1);
executeTransacction.SetCommand(new CustomerCommand(transacction1, queue));
var result = await executeTransacction.Invoke();
//Console.WriteLine($"customer#1 : {result}");
Console.WriteLine(transacction1.ToString());
//Console.WriteLine("HISTORY");
//executeTransacction.GetHistoty();

var transacction2 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer2);
executeTransacction.SetCommand(new CustomerCommand(transacction2, queue));
result = await executeTransacction.Invoke();
//Console.WriteLine($"customer#2 : {result}");
Console.WriteLine(transacction2.ToString());
//Console.WriteLine("HISTORY");
//executeTransacction.GetHistoty();

var transacction3 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer3);
executeTransacction.SetCommand(new CustomerCommand(transacction3, queue));
result = await executeTransacction.Invoke();
//Console.WriteLine($"customer#3 : {result}");
Console.WriteLine(transacction3.ToString());
//Console.WriteLine("HISTORY");
//executeTransacction.GetHistoty();

var transacction4 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer4);
executeTransacction.SetCommand(new CustomerCommand(transacction4, queue));
result = await executeTransacction.Invoke();
//Console.WriteLine($"customer#4 : {result}");
Console.WriteLine(transacction4.ToString());
//Console.WriteLine("HISTORY");
//executeTransacction.GetHistoty();

var transacction5 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer5);
executeTransacction.SetCommand(new CustomerCommand(transacction5, queue));
result = await executeTransacction.Invoke();
//Console.WriteLine($"customer#5 : {result}");
Console.WriteLine(transacction5.ToString());
//Console.WriteLine("HISTORY");
//executeTransacction.GetHistoty();

var transacction6 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer6);
executeTransacction.SetCommand(new CustomerCommand(transacction6, queue));
result = await executeTransacction.Invoke();
//Console.WriteLine($"customer#6 : {result}");
Console.WriteLine(transacction6.ToString());
//Console.WriteLine("HISTORY");
//executeTransacction.GetHistoty();

var transacction7 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer7);
executeTransacction.SetCommand(new CustomerCommand(transacction7, queue));
result = await executeTransacction.Invoke();
//Console.WriteLine($"customer#7 : {result}");
Console.WriteLine(transacction7.ToString());
//Console.WriteLine("HISTORY");
//executeTransacction.GetHistoty();

var transacction8 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer8);
executeTransacction.SetCommand(new CustomerCommand(transacction8, queue));
result = await executeTransacction.Invoke();
//Console.WriteLine($"customer#8 : {result}");
Console.WriteLine(transacction8.ToString());
//Console.WriteLine("HISTORY");
//executeTransacction.GetHistoty();

Console.WriteLine("---- COLA -----");
foreach (KeyValuePair<string, Queue<Customer>> entry in queue.GetQueues())
{
    string queueName = entry.Key;
    Queue<Customer> customerQueue = entry.Value;

    Console.WriteLine($"------------- Queue Name: {queueName} -------------");

    foreach (Customer customer in customerQueue)
    {
        Console.WriteLine(customer.ToString());
    }
}

idNewCustomer++;
Customer customer9 = new Customer(idNewCustomer.ToString(), "Susana");
idNewCustomer++;
Customer customer10 = new Customer(idNewCustomer.ToString(), "Paulina");
idNewCustomer++;
Customer customer11 = new Customer(idNewCustomer.ToString(), "Cristian");
idNewCustomer++;
Customer customer12 = new Customer(idNewCustomer.ToString(), "Monica");

var transacction9 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer9);
executeTransacction.SetCommand(new CustomerCommand(transacction9, queue));
result = await executeTransacction.Invoke();
Console.WriteLine(transacction9.ToString());

var transacction10 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer10);
executeTransacction.SetCommand(new CustomerCommand(transacction10, queue));
result = await executeTransacction.Invoke();
Console.WriteLine(transacction10.ToString());

var transacction11 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer11);
executeTransacction.SetCommand(new CustomerCommand(transacction11, queue));
result = await executeTransacction.Invoke();
Console.WriteLine(transacction11.ToString());

var transacction12 = new CustomerTransacctionInfo(Queue.Transacction.Command.Action.NoAttend, customer12);
executeTransacction.SetCommand(new CustomerCommand(transacction12, queue));
result = await executeTransacction.Invoke();
Console.WriteLine(transacction12.ToString());

foreach (KeyValuePair<string, Queue<Customer>> entry in queue.GetQueues())
{
    string queueName = entry.Key;
    Queue<Customer> customerQueue = entry.Value;

    Console.WriteLine($"------------- Queue Name: {queueName} -------------");

    foreach (Customer customer in customerQueue)
    {
        Console.WriteLine(customer.ToString());
    }
}

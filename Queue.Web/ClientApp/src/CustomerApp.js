import { useEffect, useState, useCallback } from "react";
import { ListQueues, AddClient } from "./components";
import {
  funGetQueues,
  funGetCurrentCustomer,
  funDeleteCustomer,
} from "./functions";

const CustomerApp = () => {
  const [queues, setQueues] = useState([]);
  const [nextClients, setNextClients] = useState([]);

  //Ejecutar al iniciar la app
  useEffect(() => {
    const fun = async () => {
      let queueValues = await funGetQueues();
      setQueues(queueValues);
    };

    fun();
  }, []);

  const getNextCustomers = useCallback(() => {
    let customers = [];
    queues.forEach((queue) => {
      let customer = funGetCurrentCustomer(queue);
      if (customer.customer !== 0) customers.push(customer);
    });
    return customers;
  }, [queues]);

  useEffect(() => {
    const customers = getNextCustomers();
    setNextClients(customers);
  }, [getNextCustomers]);

  useEffect(() => {
    const fun = async () => {
      for (let i = 0; i < nextClients.length; i++) {
        let client = nextClients[i];
        console.log(`Cliente a eliminar ${client.customer}`);
        let result = await funDeleteCustomer(client.customer, client.duration);
        console.log(`Resultado ELIMINAR ${result}`);
        if (result)
          console.log(
            `Cliente ${client.customer} atendido - Cola#${client.queue} Tiempo:${client.duration}`
          );
      }

      let queueValues = await funGetQueues();
      setQueues(queueValues);
    };

    fun();
  }, [nextClients]);

  return (
    <>
      <div className="container bg-dark p-4 vh-100">
        <h2 className="text-white">Usuarios</h2>

        <AddClient />

        <div className="row mt-4">
          <div className="col-sm-12">
            <div className="List-group">
              <ListQueues
                key={`list-${Math.random().toString()}`}
                queues={queues}
              />
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default CustomerApp;

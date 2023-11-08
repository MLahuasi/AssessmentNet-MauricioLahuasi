export const ListQueues = ({ queues }) => {
  //   queues.forEach((queue) => {
  //     console.log(queue);
  //     queue["customers"]["$values"].map((customer) => {
  //       console.log(customer);
  //     });
  //   });
  let arrayDeleteCustomers = [];
  queues.forEach((queue) => {
    console.log(queue);
    queue["customers"]["$values"].map((customer, index) => {
      console.log(customer);
      if (index === 0) arrayDeleteCustomers.push(customer);
    });
  });

  console.log("Eliminar de Colas");
  console.log(arrayDeleteCustomers);

  return (
    <>
      <div className="container">
        {queues.map((queue) => (
          <div className="container">
            <label className="mlmainlabel" key={`queue-${queue.id}`}>
              {queue.name} - Tiempo: {queue.duration}
            </label>
            {queue["customers"]["$values"].map((customer) => (
              <label className="mllabel" key={`customer-${customer.id}`}>
                {customer.name}
              </label>
            ))}
          </div>
        ))}
      </div>
    </>
  );
};

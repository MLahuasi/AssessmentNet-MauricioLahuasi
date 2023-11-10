export const funGetCurrentCustomer = (pQueue) => {
  let nextCustomer = {
    queue: 0,
    customer: 0,
    duration: 0,
  };

  let queue = pQueue.id;
  let duration = pQueue.duration;
  let customers = pQueue["customers"]["$values"];

  if (Array.isArray(customers)) {
    if (customers.length > 0) {
      let customer = customers[0].id;
      nextCustomer = {
        queue,
        customer,
        duration,
      };
    }
  } else {
    console.log(
      `Error: funGetCurrentCustomer - No existen registros en la cola`
    );
  }

  return nextCustomer;
};

export const funDeleteCustomer = async (id, duration) => {
  try {
    console.log(`Enviando petición Atender Cliente: ${id}`);
    let result = await new Promise((resolve, reject) => {
      setTimeout(async () => {
        try {
          let response = await fetch(
            `api/customersqueue/DeleteCustomer/${id}`,
            {
              method: "DELETE",
              headers: {
                "Content-Type": "application/json;charset=utf-8",
              },
            }
          );

          if (!response.ok) {
            console.log(
              `Error: deleteCustomer ${id} ${duration} - status code: ${response.status}`
            );
            resolve("Error");
          } else {
            resolve("OK");
          }
        } catch (error) {
          console.log(
            `Error: deleteCustomer ${id} ${duration} - Exception: ${error}`
          );
          resolve("Error");
        }
      }, duration);
    });

    return result;
  } catch (error) {
    console.log(
      `Error: deleteCustomer ${id} ${duration} - Exception: ${error}`
    );
    return "Error";
  }
};

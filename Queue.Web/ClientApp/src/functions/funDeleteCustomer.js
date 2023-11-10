export const funDeleteCustomer = async (id, duration) => {
  try {
    console.log(`Se va a eliminar el usuario ${id}`);
    let result = false;
    setTimeout(async () => {
      let response = await fetch(`api/customersqueue/DeleteCustomer/${id}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json;charset=utf-8",
        },
      });

      if (!response.ok) {
        result = true;
        console.log(
          `Error: deleteCustomer ${id} ${duration} - status code: ${response.status}`
        );
        result = false;
      }
    }, duration);

    return result;
  } catch (error) {
    console.log(
      `Error: deleteCustomer ${id} ${duration} - Exception: ${error}`
    );
  }
  return false;
};

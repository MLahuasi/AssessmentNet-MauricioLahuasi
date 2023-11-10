export const funGetQueues = async () => {
  try {
    const response = await fetch("api/customersqueue/List");

    let values = null;
    if (response.ok) {
      const data = await response.json();
      values = data["$values"];
      //   console.log(values);
    } else {
      console.log(`Error: funGetQueues -status code: ${response.status}`);
      return false;
    }

    return values;
  } catch (error) {
    console.log(`Error: funGetQueues - Exception: ${error}`);
  }
  return null;
};

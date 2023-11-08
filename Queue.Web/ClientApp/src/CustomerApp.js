import { useEffect, useState } from "react";
import { ListQueues } from "./components/ListQueues";
import { onlyNumbers } from "./helpers";

const CustomerApp = () => {
  const [customersqueue, setCustomersqueue] = useState([]);
  const [name, setName] = useState("");
  const [ci, setCi] = useState("");

  const showQueues = async () => {
    const response = await fetch("api/customersqueue/List");

    if (response.ok) {
      const data = await response.json();
      //   const id = data["$id"];
      const values = data["$values"];

      setCustomersqueue(values);
    } else {
      console.log("status code:" + response.status);
    }
  };

  //Ejecutar al iniciar la app
  useEffect(() => {
    showQueues();
  }, []);

  const saveCustomer = async (e) => {
    e.preventDefault();

    const response = await fetch("api/customersqueue/SaveCustomer", {
      method: "POST",
      headers: {
        "Content-Type": "application/json;charset=utf-8",
      },
      body: JSON.stringify({ name: name, ci: ci }),
    });

    console.log(response);

    if (response.ok) {
      setName("");
      setCi("");
      await showQueues();
    }
  };

  return (
    <>
      <div className="container bg-dark p-4 vh-100">
        <h2 className="text-white">Usuarios</h2>
        <div className="row">
          <div className="col-sm-12">
            <form onSubmit={saveCustomer}>
              {" "}
              <div className="input-group">
                {" "}
                <input
                  type="text"
                  className="form-control"
                  placeholder="Ingrese CI"
                  value={ci}
                  onChange={(e) => setCi(e.target.value)}
                  onKeyDown={(e) => onlyNumbers(e)}
                />
                <input
                  type="text"
                  className="form-control"
                  placeholder="Ingrese nombre Cliente"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
                <button className="btn btn-success">Agregar</button>
              </div>
            </form>
          </div>
        </div>

        <div className="row mt-4">
          <div className="col-sm-12">
            <div className="List-group">
              <ListQueues key={`listqueues-1`} queues={customersqueue} />
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default CustomerApp;

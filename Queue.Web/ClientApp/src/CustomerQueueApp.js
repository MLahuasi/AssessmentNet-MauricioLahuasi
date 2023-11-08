import "bootstrap/dist/css/bootstrap.min.css";
import { useEffect, useState } from "react";
import { onlyNumbers } from "./helpers";

const CustomerQueueApp = () => {
  const [customersqueue, setCustomersqueue] = useState([]);
  const [name, setName] = useState("");
  const [duration, setDuration] = useState("");

  const showQueues = async () => {
    const response = await fetch("api/customersqueue/List");

    if (response.ok) {
      const data = await response.json();
      console.log(data);
      setCustomersqueue(data);
    } else {
      console.log("status code:" + response.status);
    }
  };

  //   // const formatDate = (string) => {
  //   //     let options = { year: 'numeric', month: 'long', day: 'numeric' };
  //   //     let fecha = new Date(string).toLocaleDateString("es-PE", options);
  //   //     let hora = new Date(string).toLocaleTimeString();
  //   //     return fecha + " | " + hora
  //   // }

  //   const onlyNumbers = (e) => {
  //     const re = /[0-9]+/g;
  //     if (!re.test(e.key)) {
  //       e.preventDefault();
  //     }
  //   };

  //Ejecutar al iniciar la app
  useEffect(() => {
    showQueues();
  }, []);

  const saveQueue = async (e) => {
    e.preventDefault();

    const response = await fetch("api/customersqueue/Save", {
      method: "POST",
      headers: {
        "Content-Type": "application/json;charset=utf-8",
      },
      body: JSON.stringify({ name: name, duration: duration }),
    });

    console.log(response);

    if (response.ok) {
      setName("");
      setDuration("");
      await showQueues();
    }
  };

  const deleteQueue = async (id) => {
    const response = await fetch("api/customersqueue/Delete/" + id, {
      method: "DELETE",
    });

    if (response.ok) await showQueues();
  };
  return (
    <>
      <div className="container bg-dark p-4 vh-100">
        <h2 className="text-white">Lista de Colas</h2>
        <div className="row">
          <div className="col-sm-12">
            <form onSubmit={saveQueue}>
              {" "}
              <div className="input-group">
                {" "}
                <input
                  type="text"
                  className="form-control"
                  placeholder="Ingrese nombre cola"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
                <input
                  type="number"
                  className="form-control"
                  placeholder="Ingrese duración"
                  value={duration}
                  onChange={(e) => setDuration(e.target.value)}
                  onKeyDown={(e) => onlyNumbers(e)}
                />
                <button className="btn btn-success">Agregar</button>
              </div>
            </form>
          </div>
        </div>

        <div className="row mt-4">
          <div className="col-sm-12">
            <div className="List-group">
              {customersqueue.map((item) => (
                <div
                  key={`customersqueue-${item.id}`}
                  className="list-group-item list-group-item-action"
                >
                  <h5 className="text-primary">{item.name}</h5>
                  <div className="d-flex justify-content-between">
                    <small className="text-muted">
                      Duración: {item.duration}
                    </small>
                    <button
                      type="button"
                      className="btn btn-sm btn-outline-danger"
                      onClick={() => deleteQueue(item.id)}
                    >
                      Eliminar
                    </button>
                  </div>
                  ;
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default CustomerQueueApp;

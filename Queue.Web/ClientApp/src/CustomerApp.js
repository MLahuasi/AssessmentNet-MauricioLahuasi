import { useEffect, useState } from "react";
import { ListQueues } from "./components/ListQueues";
import { onlyNumbers } from "./helpers";

const CustomerApp = () => {
  const [customersqueue, setCustomersqueue] = useState([]);
  const [name, setName] = useState("");
  const [ci, setCi] = useState("");
  const [timeColaUno, setTimeColaUno] = useState(0);
  const [timeColaDos, setTimeColaDos] = useState(0);

  //Ejecutar al iniciar la app
  useEffect(() => {
    showQueues();
  }, []);

  const showQueues = async () => {
    const response = await fetch("api/customersqueue/List");

    if (response.ok) {
      const data = await response.json();
      const values = data["$values"];
      console.log(values);
      setCustomersqueue(values);
      setTiempos();
    } else {
      console.log("status code:" + response.status);
    }
  };

  useEffect(() => {
    const ejecutarDeleteCustomer = async () => {
      console.log("useEffect - se ejecutó Cola#1");
      console.log(`Tiempo: ${timeColaUno}`);
      try {
        let idNexUno = customersqueue[0]["customers"]["$values"][0].id;
        await deleteCustomer(idNexUno);
      } catch (error) {
        console.log(`Captura error ejecutarDeleteCustomer#1: ${error}`);
      }
    };

    const idIntervalo = setInterval(
      () => {
        ejecutarDeleteCustomer();
      },
      timeColaUno > 0 ? timeColaUno : 10000
    );

    // Limpiar el intervalo cuando el componente se desmonte
    return () => {
      clearInterval(idIntervalo);
    };
  }, []);

  useEffect(() => {
    const ejecutarDeleteCustomer = async () => {
      console.log("useEffect - se ejecutó Cola#2");
      console.log(`Tiempo: ${timeColaDos}`);
      try {
        let idNexDos = customersqueue[1]["customers"]["$values"][0].id;
        await deleteCustomer(idNexDos);
      } catch (error) {
        console.log(`Captura error ejecutarDeleteCustomer#2: ${error}`);
      }
    };

    const idIntervalo = setInterval(
      () => {
        ejecutarDeleteCustomer();
      },
      timeColaDos > 0 ? timeColaDos : 10000
    );

    // Limpiar el intervalo cuando el componente se desmonte
    return () => {
      clearInterval(idIntervalo);
    };
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

  const deleteCustomer = async (id) => {
    try {
      const response = await fetch(`api/customersqueue/DeleteCustomer/${id}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json;charset=utf-8",
        },
      });

      if (response.ok) {
        await showQueues();
      } else {
        console.log("status code:" + response.status);
      }
    } catch (error) {
      console.log(`Error Insertar ${error}`);
    }
  };

  const setTiempos = () => {
    try {
      const tUno = customersqueue[0].duration;
      const tDos = customersqueue[1].duration;
      setTimeColaUno(tUno);
      setTimeColaDos(tDos);
      console.log(`Tiempo#1 ${tUno} - Tiempo#2 ${tDos}`);
    } catch (error) {
      setTimeColaUno(10000);
      setTimeColaDos(10000);
      console.log(`Tiempo#1 ${10000} - Tiempo#2 ${10000}`);
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
              <ListQueues
                key={`list-${Math.random().toString()}`}
                queues={customersqueue}
              />
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default CustomerApp;

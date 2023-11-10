import { useState } from "react";
import { onlyNumbers } from "../helpers";

export const AddClient = () => {
  const [name, setName] = useState("");
  const [ci, setCi] = useState("");

  const saveCustomer = async (e) => {
    //e.preventDefault();
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
    }
  };

  return (
    <>
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
    </>
  );
};

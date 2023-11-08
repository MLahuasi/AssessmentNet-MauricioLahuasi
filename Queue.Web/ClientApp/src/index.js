import React from "react";
import ReactDOM from "react-dom/client";
import "bootstrap/dist/css/bootstrap.min.css";
import "./custom.css";

// import CustomerQueueApp from "./CustomerQueueApp";
import CustomerApp from "./CustomerApp";

//Vamos a crear un root /elemento raiz donde deseamos que se renderize/pinte nuestros componentes
const root = ReactDOM.createRoot(document.getElementById("root"));

root.render(<CustomerApp />);

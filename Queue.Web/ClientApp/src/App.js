import "bootstrap/dist/css/bootstrap.min.css"
import { useEffect, useState } from "react";

const App = () => {
    const [customersqueue, setCustomersqueue] = useState([]);

    const showQueues = async () => {

        const response = await fetch("api/customersqueue/Lista");

        if (response.ok) {
            const data = await response.json();
            console.log(data)
            setCustomersqueue(data)
        } else {
            console.log("status code:" + response.status)
        }

    }

    const formatDate = (string) => {
        let options = { year: 'numeric', month: 'long', day: 'numeric' };
        let fecha = new Date(string).toLocaleDateString("es-PE", options);
        let hora = new Date(string).toLocaleTimeString();
        return fecha + " | " + hora
    }

    /// AQUI:: min 0:19:13
    //Ejecutar al iniciar la app
    useEffect(() => {
        showQueues();
    }, [])

    return (
        <div className="container bg-dark p-4 vh-100">
            <h2 className="text-white">Lista de tareas</h2>
            <div className="row">
                <div className="col-sm-12">

                </div>
            </div>

            <div className="row mt-4">
                <div className="col-sm-12">
                    <div className="List-group">
                        {
                            customersqueue.map(
                                (item) => (
                                    <div key={`customersqueue-${item.id}`} className="list-group-item list-group-item-action">
                                        <h5 className="text-primary">{item.name} {item.duration}</h5>
                                    </div>
                                ) 
                            )
                        }
                    </div>
                </div>
            </div>
        </div>
    )
}

export default App;
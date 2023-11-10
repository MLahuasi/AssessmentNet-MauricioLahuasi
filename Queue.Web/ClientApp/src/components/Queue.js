export const Queue = ({ queue }) => {
  return (
    <>
      <div className="container">
        {
          <div className="container">
            <label className="mlmainlabel" key={`queue-${queue.id}`}>
              {queue.name} - Tiempo: {queue.duration}
            </label>
            {queue["customers"]["$values"].map((customer) => (
              <label className="mllabel" key={`customer-${customer.id}`}>
                {customer.name}
              </label>
            ))}
          </div>
        }
      </div>
    </>
  );
};

import { Queue } from "./";

export const ListQueues = ({ queues }) => {
  return (
    <>
      <div className="container">
        {queues.map((queue) => (
          <Queue key={`queue-${queue.id}`} queue={queue} />
        ))}
      </div>
    </>
  );
};

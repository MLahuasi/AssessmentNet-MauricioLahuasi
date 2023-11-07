using Queue.Transacction.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Transacction.Invoker
{
    public class ExecuteTransactions
    {
        private List<ICommand> _commands;
        private ICommand _command;

        public ExecuteTransactions()
        {
            _commands = new List<ICommand>();
        }

        public void SetCommand(ICommand command) => _command = command;
        public async Task<bool> Invoke()
        {
            //Rastreo de Acciones
            var result = await _command.Execute();
            if(result != null)
            {
                _commands.Add(_command);
                return true;
            }
            
            return false;
        }

        public void GetHistoty()
        {
            foreach (var command in _commands)
            {
                Console.WriteLine(command.ToString());
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ICommandHandlerAsync<TCommand>
        where TCommand : struct
    {
        Task HandleAsync(TCommand command);
    }
}

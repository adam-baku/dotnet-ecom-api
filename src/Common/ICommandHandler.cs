using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface ICommandHandler<TCommand>
        where TCommand : struct
    {
        public void Handle(TCommand command);
    }
}

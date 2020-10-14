using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public abstract class EntityAbstract
    {
        public Guid EntityId { get; protected set; }
    }
}

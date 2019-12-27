using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Contracts.Commands.Base
{
    public class CommandBase
    {
        public Guid Id { get; }

        public CommandBase()
        {
            Id = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            Id = id;
        }
    }
}

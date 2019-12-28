using MailService.Common.Bus.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Contracts.Commands.Base
{
    public class CommandBase : ICommand
    {
        public Guid CommandId { get; }

        public CommandBase()
        {
            CommandId = Guid.NewGuid();
        }

        protected CommandBase(Guid commandId)
        {
            CommandId = commandId;
        }
    }
}

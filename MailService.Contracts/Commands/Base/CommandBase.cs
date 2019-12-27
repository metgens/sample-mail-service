using MailService.Common.Bus.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Contracts.Commands.Base
{
    public class CommandBase : ICommand
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

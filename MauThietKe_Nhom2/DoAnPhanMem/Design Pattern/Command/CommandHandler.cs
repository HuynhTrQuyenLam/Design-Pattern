using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
namespace DoAnPhanMem.Command
{
    public class CommandHandler
    {
        public void Handle(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            command.Execute();
        }
    }
}

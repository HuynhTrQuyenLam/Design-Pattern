using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DoAnPhanMem.Command
{
    public interface ICommand
    {
        void Execute();
    }
}

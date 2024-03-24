using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnPhanMem.Proxy
{
    public interface IAccount
    {
        void Register(Account account);
    }
}

using DoAnPhanMem;
using DoAnPhanMem.Controllers;
using DoAnPhanMem.Models;
using DoAnPhanMem.Proxy;
using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

public class AccountProxy : IAccount
{
    private readonly IAccount _realSubject;
    private WebshopEntities db = new WebshopEntities();

    public AccountProxy(IAccount realSubject)
    {
        _realSubject = realSubject;
    }


    public void Register(Account account)
    {
        var checkEmail = db.Accounts.FirstOrDefault(m => m.email == account.email);
        if (checkEmail != null)
        {
            throw new Exception("Email đã được sử dụng!!!");
        }
        else
        {
            _realSubject.Register(account);
        }
    }


}

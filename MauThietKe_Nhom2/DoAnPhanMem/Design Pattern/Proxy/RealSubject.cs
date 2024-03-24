using DoAnPhanMem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnPhanMem.Proxy
{
    public class RealSubject : IAccount
    {
        private WebshopEntities db = new WebshopEntities();

        public void Register(Account account)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    int roleId = db.Roles.FirstOrDefault(role => role.role_name == "Người dùng")?.role_id ?? 0;
                    account.acc_status = "1";
                    account.role_id = roleId;
                    account.avatar = "/Content/Images/avatar/default.jpg";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Đăng ký thất bại: " + ex.Message);
                }
            }
        }
    }

}


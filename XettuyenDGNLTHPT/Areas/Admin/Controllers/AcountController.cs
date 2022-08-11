using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using XettuyenDGNLTHPT.Areas.Admin.Middleware;
using XettuyenDGNLTHPT.Models;

namespace XettuyenDGNLTHPT.Areas.Admin.Controllers
{
    public class AcountController : Controller
    {
        SEP25Team08Entities model = new SEP25Team08Entities();
        // GET: Admin/Login_Logout
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username,string password)
        {
            var user = model.Accounts.FirstOrDefault(u => u.Username.Equals(username));
            if(user != null)
            {
                if (user.Password.Equals(password))
                {
                    Session["usser-fullname"] = user.Full_Name;
                    Session["user-id"] = user.ID;
                    Session["user-role"] = user.Role;
                    return RedirectToAction("Index","HomeAdmin");
                }
                else
                {
                    Session["password-incorrect"] = true;
                    return View();
                }
            }
            Session["username-incorrect"] = true;
            return View();
        }
        public ActionResult Logout(string confirm_value)
        {
            
          Session["usser-fullname"] = null;
         Session["user-id"] = null;
         Session["user-role"] = null;
         return View("Login");
  
        }
        [LoginVerification]
        public ActionResult AddAccount()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddAccount(Account account, string repass, string role)
        {
            var thongtin = account;
            if (account.Password.Equals(repass))
            {
                if (role.Equals("1"))
                {
                    thongtin.Role = "1";
                    model.Accounts.Add(thongtin);
                    model.SaveChanges();
                    return RedirectToAction("Index","HomeAdmin");
                }
                else
                {
                    thongtin.Role = "0";
                    model.Accounts.Add(thongtin);
                    model.SaveChanges();
                    return RedirectToAction("Index", "HomeAdmin");
                }
            }
            else
            {
                
            }
            
            return View();
        }
        [LoginVerification]
        public ActionResult EditAccount(int id)
        {
            var thongtin = model.Accounts.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(thongtin);
        }
        [HttpPost]
        public ActionResult EditAccount(Account account, string repass, string role)
        {
            if (ModelState.IsValid)
            {
                var thongtin = account;
                if (account.Password.Equals(repass))
                {
                    if (role.Equals("1"))
                    {
                        
                        thongtin.Role = "1";
                        model.Entry(thongtin).State = EntityState.Modified;
                        model.SaveChanges();
                        return RedirectToAction("Index", "HomeAdmin");
                    }
                    else
                    {
                        SEP25Team08Entities db = new SEP25Team08Entities();
                        var roletk = db.Accounts.ToList();
                        int count = roletk.Count(u => u.Role == "1");
                        if(count <= 1)
                        {
                            ViewBag.ErrorMessage = "Cần có 1 quản trị";
                            return View();
                        }
                        else
                        {
                            thongtin.Role = "0";
                            model.Entry(thongtin).State = EntityState.Modified;
                            model.SaveChanges();
                            return RedirectToAction("Index", "HomeAdmin");
                        }

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không trùng khớp");
                }
            }
            return View();
        }
        [LoginVerification]
        public ActionResult DeleteAccount(int id)
        {
            var xoa = model.Accounts.Find(id);
            model.Accounts.Remove(xoa);
            model.SaveChanges();
            return RedirectToAction("ManageAcount","HomeAdmin");
        }


    }
}
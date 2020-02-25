using BAL;
using DataModels;
using DataModels.Enum;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                AccountManager accountManager = new AccountManager();
                User user = accountManager.ValidateUser(userModel.UserName, userModel.Password);

                if (user != null)
                {
                    string userData = userModel.UserName + "$" + user.Id;

                    FormsAuthentication.SetAuthCookie(userData, false);

                    var authTicket = new FormsAuthenticationTicket(1, userData, DateTime.Now,
                        DateTime.Now.AddMinutes(20), false, GetRoleNameById(user.RoleId));
                    string encryptedticket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedticket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    return RedirectToAction("GetAll", "Task");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(userModel);
                }
            }

            return View();
        }

        [HttpPost]
        public JsonResult IsUserNameNotExists(string userName)
        {
            return Json(!new AccountManager().IsUserExists(userName));
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        private string GetRoleNameById(int roleId)
        {
            return roleId == (int)UserType.Manager ? "Manager" : roleId == (int)UserType.Developer ? "Developer" :
                roleId == (int)UserType.Tester ? "Tester" : "";
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserRegisterModel userRegisterModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new AccountManager().SaveUser(
                        new User()
                        {
                            Id = Guid.NewGuid(),
                            ContactNo = userRegisterModel.ContactNo,
                            CreatedDate = DateTime.Now,
                            EmailId = userRegisterModel.EmailId,
                            IsActive = true,
                            Name = userRegisterModel.Name,
                            Password = userRegisterModel.Password,
                            RoleId = (int)UserType.Manager,
                            UserName = userRegisterModel.UserName
                        }
                    );
                    ViewData["IsUserSaved"] = true;
                }
                catch
                {
                    ViewData["IsUserSaved"] = false;
                }
            }
            return View();
        }
    }
}

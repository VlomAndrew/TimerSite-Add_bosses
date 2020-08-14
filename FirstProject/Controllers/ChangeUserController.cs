using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using FirstProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace FirstProject.Controllers
{
    public class ChangeUserController : Controller
    {
        // GET: ChangeUser
       
        public ActionResult Index()
        {
            var uc = new ApplicationDbContext();
            var userList = uc.Users.ToList();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            
            Dictionary<string, string> users = new Dictionary<string, string>();
            foreach (var user in userList)
            {
                var email = user.Email;
                var nick = user.UserName;
                
                var us = userManager.FindByEmail(email);
                if (us != null)
                {
                    var roles = userManager.GetRoles(us.Id);
                    if (roles.Count<1) continue;
                    
                    users.Add(string.Format("{0}/{1}", email, nick), roles[0]);
                }
                
            }
            ViewBag.users = users;
            return View();
        }




        
        [HttpGet]
        public async Task<ActionResult> Edit(string email)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            email = email.Remove(email.IndexOf('/'));
            var rs = rm.Roles;
            List<string> roles = new List<string>();
            foreach (var r in rs)
            {
                roles.Add(r.Name);   
            }
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                ViewBag.roles = roles.ToList();
                ViewBag.user = user;
                ViewBag.userRole = await userManager.GetRolesAsync(user.Id);
                
                return View();
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<ActionResult> Edit(string email,string nick,string role,string pass=null)
        {
            
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                
                user.Email = email;
                user.UserName = nick;
                await userManager.RemoveFromRoleAsync(user.Id, userManager.GetRoles(user.Id)[0]);
                await userManager.AddToRoleAsync(user.Id, role);
                if (pass != null && pass!=string.Empty)
                {

                    var res = await userManager.UpdateAsync(user);
                    if (res.Succeeded)
                    {
                        await userManager.RemovePasswordAsync(user.Id);
                        await userManager.AddPasswordAsync(user.Id, pass);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("","Пользователь не обновлен");
                    }
                    //userManager.ResetPasswordAsync(user.Id, u.GetPassword() , pass);

                }
                else
                {
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("","Пользователь не обновлен");

                    }
                }
            }
            else
            {
                        ModelState.AddModelError("","Пользователь не найден");

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.NickName, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user.Id, "Solder");
                    

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
            
        }
       
        public async Task<ActionResult> Delete(string email)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            email = email.Remove(email.IndexOf('/'));
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }
    }
}
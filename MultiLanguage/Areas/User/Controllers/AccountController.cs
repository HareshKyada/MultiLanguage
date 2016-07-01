using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MultiLanguage.Attribute;
using MultiLanguage.Infrastructure;
using MultiLanguage.User.Models;

namespace MultiLanguage.User.Controllers
{
    [Authorize]
    [Localization]
    public class AccountController : BaseController
    {

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {

                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {

                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}

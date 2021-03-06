﻿using System;
using AutoMapper;
using MaintenanceTracker.Domain;
using MaintenanceTracker.Domain.Model;
using MaintenanceTracker.Web.ViewModels;
using System.Web.Mvc;

namespace MaintenanceTracker.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserStore _userStore;
        private IFormsAuthenticationService _authenticationService;

        public UserController(IUserStore userStore, IFormsAuthenticationService authService)
        {
            _userStore = userStore;
            _authenticationService = authService;
        }

        public ActionResult Index()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_userStore.Authenticate(model.Username, model.Password))
            {
                _authenticationService.SetAuthCookie(model.Username, model.RememberMe);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("WrongUsernamePassword", "Invalid Username and Password");
            
            return View(model);
        }

        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("Error", "Your passwords must match");
                return View(model);
            }

            var user = Mapper.Map<User>(model);
            try
            {
                _userStore.AddUser(user, model.Password);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("Error", e.Message);
                return View(model);
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}
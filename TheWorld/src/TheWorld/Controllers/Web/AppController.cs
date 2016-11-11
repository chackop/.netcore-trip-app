﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheWorld.Model;
using TheWorld.Servces;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService,
        IConfigurationRoot config,
        IWorldRepository repository,
        ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();

        }

        [Authorize]
        public IActionResult Trips()
        {
            try
            {
                //var data = _repository.GetAllTrips();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get trips for index page: { ex.Message }");
                return Redirect("/error");
            }

        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("", "No AOL Support");
            }

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "testsubject", model.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Message send";
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

    }
}

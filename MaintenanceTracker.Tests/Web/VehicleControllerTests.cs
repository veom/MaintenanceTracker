﻿using System.Linq;
using System.Collections.Generic;
using MaintenanceTracker.Domain;
using MaintenanceTracker.Domain.Model;
using MaintenanceTracker.Tests.Domain.Context;
using MaintenanceTracker.Web.Controllers;
using NUnit.Framework;

namespace MaintenanceTracker.Tests.Web
{
    [TestFixture]
    public class VehicleControllerTests
    {
        private IMaintenanceTrackerContext _context;

        public VehicleControllerTests()
        {
            _context = new MaintenanceTrackerContext();
            _context.Users.Add(new User
            {
                Id = 1,
                Username = "test"
            });

            var make = new Make
            {
                Id = 1,
                Name = "make"
            };

            var model = new Model
            {
                Id = 1,
                Name = "model",
                Make = make
            };

            var user = new User
            {
                Id = 1,
                Email = "Test"
            };

            make.Models = new List<Model> {model};

            _context.Models.Add(model);
            _context.Makes.Add(make);

            _context.Vehicles.Add(new Vehicle
            {
                Id = 1,
                Year = "2015",
                Kilometres = 100,
                Model = model,
                User = user
            });
            _context.Vehicles.Add(new Vehicle
            {
                Id = 2,
                Year = "2014",
                Kilometres = 1000,
                Model = model,
                User = user
            });

            user.Vehicles.Add(_context.Vehicles.First());
            user.Vehicles.Add(_context.Vehicles.Last());
        }

        [Test]
        public void Get_Returns_Vehicles_For_User_Only()
        {
            var controller = new VehicleController(_context);

            var result = controller.Get();

            res
        }

        [Test]
        public void Gets_All_Vehicles()
        {
            
        }

        [Test]
        public void Gets_Individual_Vehicle()
        {
            
        }

        [Test]
        public void Creates_New_Vehicle()
        {
            
        }
    }
}
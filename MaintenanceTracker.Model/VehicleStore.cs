﻿using MaintenanceTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaintenanceTracker.Domain
{
    public class VehicleStore : IVehicleStore, IDisposable
    {
        protected IMaintenanceTrackerContext _context;

        public VehicleStore(IMaintenanceTrackerContext context)
        {
            _context = context;
        }

        public void AddVehicle(string username, Vehicle vehicle)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
                throw new ArgumentException("Invalid User");

            vehicle.User = user;

            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void AddModel(Model.Model model)
        {
            if (_context.Models.Any(m => m.Name == model.Name && m.Make == model.Make))
                throw new ArgumentException("Model already exists");

            _context.Models.Add(model);
            _context.SaveChanges();
        }

        public void AddMake(Make make)
        {
            if (_context.Makes.Any(m => m.Name == make.Name))
                throw new ArgumentException("Make already exists");

            _context.Makes.Add(make);
            _context.SaveChanges();
        }

        public void DeleteVehicle(int userId, int vehicleId)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == vehicleId);

            if (vehicle == null)
                throw new ArgumentException("Invalid Vehicle Id");

            if (vehicle.User.Id != userId)
                throw new ArgumentException("Invalid user id");

            _context.Vehicles.Remove(vehicle);
        }

        public IEnumerable<Vehicle> ListVehicles(string username)
        {
            return _context.Vehicles.Where(v => v.User.Username == username);
        }

        public IEnumerable<Model.Model> ListModels()
        {
            return _context.Models;
        }

        public IEnumerable<Make> ListMakes()
        {
            return _context.Makes;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}

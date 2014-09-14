﻿using MaintenanceTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaintenanceTracker.Model
{
    public interface IMaintenanceStore
    {
        void AddSchedule(int vehicleId, Schedule schedule);
        void AddTask(int vehicleId, Task task);
        IEnumerable<Schedule> GetSchedules(int vehicleId);
        IEnumerable<Task> GetTasks(int vehicleId);
    }
}
using EmcureCERI.Business.Contract;
using EmcureCERI.Business.Core;
using EmcureCERI.Business.Models.DataModel;
using EmcureCERI.ConsoleApp.Models;
using EmcureCERI.ConsoleApp.Services;
using EmcureCERI.Data.DataAccess.Entities;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmcureCERI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //using fluentscheduler for check and mail every day for patient age is greater than 18 or not 
            var registry = new Registry();
            JobManager.Initialize(registry);
            ScheduledService scheduledService = new ScheduledService();
            JobManager.AddJob(() => scheduledService.SendEmails(), s => s
           .ToRunEvery(1)
           .Days().At(00,05));
            //.ToRunNow());

            // Wait for something
            Console.WriteLine("Press enter to terminate...");
            Console.ReadLine();

            // Stop the scheduler
            JobManager.StopAndBlock();
        }
    }
}

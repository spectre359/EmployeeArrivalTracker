using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Reporting.Contracts.Misc;
using Reporting.Data.Context;
using Reporting.Data.Entities;
using Reporting.Repositories;
using Reporting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.EmployeeSeeder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var serviceProvider = new ServiceCollection()  
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddDbContext<ReportingToolContext>(options => options.UseSqlServer(connectionString))     
            .BuildServiceProvider();
            Console.WriteLine("Searching for employees.json file...");
            using (StreamReader r = new StreamReader("employees.json"))
            {
                Console.WriteLine("File found");
                string json = r.ReadToEnd();
                List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(json);
                Console.WriteLine($"Deserialization complete. Employees found: {employees.Count()}. Saving to database...This may take several minutes.");
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
                await unitOfWork.Employees.AddMany(employees);
                await unitOfWork.SaveAsync();
                Console.WriteLine("Seeding complete!");
                Console.WriteLine("You may close this application");
                Console.ReadLine();
            }
        }
    }
}

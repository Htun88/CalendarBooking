using Application;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlTypes;

public class Program
{

    static void Main(string[] args)
    {
        //CreateHostBuilder(args).Build().Run();
        // Build configuration
        //IConfiguration configuration = new ConfigurationBuilder()
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    .Build();

        // Setup DI container
        var serviceProvider = ConfigureServices();
        // Setup DI container
        //var serviceProvider = new ServiceCollection()
        //    .AddTransient<IAppointmentRepository, AppointmentRepository>()
        //    .AddTransient<IAppointmentService, AppointmentService>()
        //    .BuildServiceProvider();

        // Configure logging
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Information);
        });

        var logger = loggerFactory.CreateLogger<Program>();

        // Get connection string from configuration
        //var connectionString = configuration.GetConnectionString("DefaultConnection");


        // Resolve the service

        var appointmentRepository = serviceProvider.GetService<IAppointmentRepository>();
        var appointmentService = serviceProvider.GetService<IAppointmentService>();

        var commandParser = new CommandParser(appointmentService, appointmentRepository);

        while (true)
        {
            Console.WriteLine("Enter command (ADD, DELETE, FIND, KEEP, EXIT):");
            var command = Console.ReadLine();
           

            if (!string.IsNullOrEmpty(command)){

                var inputCommand = command.ToUpper();
                switch (inputCommand)
                {
                    case "ADD":
                        Console.WriteLine("Enter datetime: DD/MM hh:mm (24 hours format only)");
                        break;
                    case "DELETE":
                        Console.WriteLine("Enter datetime: DD/MM hh:mm (24 hours format only)");
                        break;
                    case "FIND":
                        Console.WriteLine("Enter datetime: DD/MM");
                        break;
                    case "KEEP":
                        Console.WriteLine("Enter datetime: hh:mm (24 hours format only)");
                        break;
                    case "EXIT":
                        Environment.Exit(0);
                        break;
                     default:
                        
                       Console.WriteLine("Invalid command");
                        Environment.Exit(0);
                        break;
                }

                
                var dateTimeString = Console.ReadLine();
                var stringSplit = dateTimeString.Split(' ');
                var datetime = string.Empty;

                if (stringSplit.Length < 2 && dateTimeString.Contains(":"))
                {
                    DateTime currentDate = DateTime.Today;                   
                    datetime = currentDate.ToString("dd/MM/yyyy") + " " + dateTimeString;

                }
                else if(stringSplit.Length < 2 && dateTimeString.Contains("/"))
                {
                    datetime = dateTimeString + " " + DateTime.Parse(dateTimeString).ToString("HH:MM");
                }
                else
                {
                    datetime = dateTimeString;
                }


                  if (!string.IsNullOrWhiteSpace(datetime) && !string.IsNullOrWhiteSpace(command))
                    {
                        commandParser.Parse(datetime, command);
                    }
                
            }
          
        }

        //public static Microsoft.Extensions.Hosting.IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        static IServiceProvider ConfigureServices()
        {
            IConfiguration configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();
            var services = new ServiceCollection();

            services.AddDbContext<AppointmentContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register AppointmentRepository for DI
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddSingleton<IAppointmentRepository, AppointmentRepository>();

            // Other service registrations...

            return services.BuildServiceProvider();
        }
    }



}
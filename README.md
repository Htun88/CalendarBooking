# Appointment Scheduler

This is a simple console application written in C# that allows users to manage appointments via command line interface. The application uses SQL Server Express LocalDB for storing appointment data.

## Features

- Add appointments with specified date and time.
- Delete appointments by date and time.
- Find available time slots for a given day.
- Keep a time slot available for any day.
- Time slots are always 30 minutes long.
- Time slots are restricted between 9AM and 5PM, except from 4 PM to 5 PM on each second day of the third week of any month, which must be reserved and unavailable.

## Requirements

- .NET Framework or .NET Core
- SQL Server Express LocalDB

## Installation

1. Clone the repository:

git clone <repository-url>


2. Open the solution in Visual Studio or your preferred IDE.
3. Build the solution.
4. Ensure SQL Server Express LocalDB is installed on your machine. If not, you can download and install it from the [official website](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

## Usage

1. Run the application from the command line or IDE.

2. Use the following commands to manage appointments:

   - `ADD DD/MM hh:mm` to add an appointment.
   - `DELETE DD/MM hh:mm` to remove an appointment.
   - `FIND DD/MM` to find available time slots for the day.
   - `KEEP hh:mm` to keep a time slot available for any day.
   - `EXIT` to exit the application.

   Replace `DD/MM` with the date in day/month format (e.g., 17/04), and `hh:mm

## Example
Enter command (ADD, DELETE, FIND, KEEP, EXIT):
ADD 
Enter datetime: DD/MM hh:mm (24 hours format only)
17/04 14:00
Appointment added successfully.

## Contributing

Contributions are welcome! Feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License - 

   

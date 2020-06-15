using System;

namespace MongoDBSampleProject
{
    partial class Program
    {
        public static class ConsoleInputHelper
        {
            public static string GetStringFromConsole(string displayLine)
            {
                Console.Write(displayLine);
                return Console.ReadLine();
            }
            public static Guid GetGuidFromConsole(string displayLine)
            {
                string id = GetStringFromConsole(displayLine);
                return Guid.Parse(id);
            }
            public static int GetIntFromConsole(string displayLine)
            {
                string consoleInput = GetStringFromConsole(displayLine);
                return int.Parse(consoleInput);
            }
            public static DateTime GetDateTimeFromConsole()
            {
                Console.WriteLine("Enter Date of birth");
                int year = GetIntFromConsole("Enter year: ");
                int month = GetIntFromConsole("Enter month: ");
                int day = GetIntFromConsole("Enter day: ");
                return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
            }
        }
    }
}

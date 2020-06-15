using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace MongoDBSampleProject
{
    partial class Program
    {

        public static void InsertPerson()
        {
            string firstName = ConsoleInputHelper.GetStringFromConsole("Enter first name: ");
            string lastName = ConsoleInputHelper.GetStringFromConsole("Enter last name: ");

            PersonModel person = new PersonModel
            {
                FirstName = firstName,
                LastName = lastName,
            };

            var mongoDB = new MongoCRUD("AddressBook");
            mongoDB.InsertRecord("Users", person);
            
        }
        public static void UpdatePerson()
        {
            Guid personGuid = ConsoleInputHelper.GetGuidFromConsole("Enter Guid of Person: ");
            DateTime dob = ConsoleInputHelper.GetDateTimeFromConsole();

            var mongoDB = new MongoCRUD("AddressBook");
            var person = mongoDB.LoadRecordById<PersonModel>("Users", personGuid);
            person.DateOfBirth = dob;
            mongoDB.UpsertRecord("Users", person.Id, person);
        }
        public static void DeletePerson()
        {
            Guid personGuid = ConsoleInputHelper.GetGuidFromConsole("Enter Guid of Person: ");
            var mongoDB = new MongoCRUD("AddressBook");
            var personFound = mongoDB.LoadRecordById<PersonModel>("Users", personGuid);
            if (personFound != null) mongoDB.DeleteRecord<PersonModel>("Users", personGuid);
        }

        public static void GetAllPeople()
        {
            var mongoDB = new MongoCRUD("AddressBook");
            var records = mongoDB.LoadRecords<PersonModel>("Users");
            foreach (var record in records)
            {
                Console.WriteLine($"{ record.Id }: { record.FirstName } { record.LastName }");
                if (record.PrimaryAddress != null) Console.WriteLine(record.PrimaryAddress.City);
                if (record.DateOfBirth.Year != 1) Console.WriteLine(record.DateOfBirth);
            }
        }
        public static void GetAllPeoplefirstAndLastNameOnly()
        {
            var mongoDB = new MongoCRUD("AddressBook");
            var records = mongoDB.LoadRecords<NameModel>("Users");
            foreach (var record in records)
            {
                Console.WriteLine($"{ record.FirstName } { record.LastName }");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Display all people");
            GetAllPeople();
            Console.WriteLine("------------------");


            Console.WriteLine("Insert new person");
            InsertPerson();
            Console.WriteLine("Record Inserted");


            Console.WriteLine("Display all people");
            GetAllPeople();
            Console.WriteLine("------------------");


            Console.WriteLine("Updating person");
            UpdatePerson();
            Console.WriteLine("Record Updated");


            Console.WriteLine("Display all people");
            GetAllPeople();
            Console.WriteLine("------------------");


            Console.WriteLine("Delete person by Id");
            DeletePerson();
            Console.WriteLine("Record Deleted");


            Console.WriteLine("Display all people");
            GetAllPeople();
            Console.WriteLine("------------------");


            Console.WriteLine("Display all people only first and last name");
            GetAllPeoplefirstAndLastNameOnly();
            Console.WriteLine("------------------");

            Console.ReadKey();
        }
    }
}

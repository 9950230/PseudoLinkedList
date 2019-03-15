using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace JSONToObject
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch swGetJSON = new Stopwatch();
            swGetJSON.Start();

            // Task One:
            //string json = GetJSONString("https://uinames.com/api/?ext");

            // Task Two / Three:
            string json = GetJSONString("https://uinames.com/api/?ext&amount=10");

            // Test WebClient Exceptions:
            //string json = GetJSONString("htps://uinames.com/api/?ext&amount=10");

            // Test Deserialization Exceptions
            //string json = GetJSONString("https://google.com");

            swGetJSON.Stop();


            Stopwatch swJSONToObjects = new Stopwatch();
            swJSONToObjects.Start();

            // Deserialization requires the JSON To be wrapped in [ ]
            if (json.Length > 0 && json[0].ToString() != "[")
                json = "[" + json + "]";

            // Create and populate Person objects from the JSON
            // This will automatically change key names (example: name -> Name but will NOT change credit_card to CreditCard)
            List<Person> persons = DeserializeJSONString<Person>(json);

            swJSONToObjects.Stop();


            if (persons != null)
            {

                // Build task one / two                
                Stopwatch swTaskOneTwo = new Stopwatch();
                swTaskOneTwo.Start();

                StringBuilder sb = new StringBuilder();
                foreach (Person person in persons)
                {
                    sb.AppendFormat("Full Name:  {0}", person.FullName);
                    sb.AppendFormat("\nTitle:      {0}", person.Title);
                    sb.AppendFormat("\nFirst Name: {0}", person.Name);
                    sb.AppendFormat("\nSurname:    {0}", person.Surname);
                    sb.AppendFormat("\nGender:     {0}", person.Gender);
                    sb.AppendFormat("\nRegion:     {0}", person.Region);
                    sb.AppendFormat("\nAge:        {0}", person.Age);
                    sb.AppendFormat("\nPhone:      {0}", person.Phone);
                    sb.AppendFormat("\nBirthday:   {0} (mm/dd/yyyy: {1}) (Raw: {2})", person.Birthday.Dmy, person.Birthday.Mdy, person.Birthday.Raw);
                    sb.AppendFormat("\nEmail:      {0}", person.Email);
                    sb.AppendFormat("\nPassword:   {0}", person.Password);
                    sb.AppendFormat("\nPhoto:      {0}", person.Photo);
                    sb.AppendFormat("\nCredit Card:");
                    sb.AppendFormat("\n>> Expiration: {0}", person.CreditCard.Expiration);
                    sb.AppendFormat("\n>> Number:     {0}", person.CreditCard.Number);
                    sb.AppendFormat("\n>> Pin:        {0}", person.CreditCard.Pin);
                    sb.AppendFormat("\n>> Security:   {0}", person.CreditCard.Security);
                    sb.AppendFormat("\n\n***********************************\n\n");
                }

                // Display task one / two
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("################# Task One / Two ##################\n");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(sb);

                swTaskOneTwo.Stop();

                Console.WriteLine("Time to get the JSON: {0}ms", swGetJSON.ElapsedMilliseconds);
                Console.WriteLine("Time to Populate the objects from JSON: {0}ms", swJSONToObjects.ElapsedMilliseconds);
                Console.WriteLine("Time to build the string and display it: {0}ms", swTaskOneTwo.ElapsedMilliseconds);
                Console.WriteLine("Combined Time: {0}ms\n\n", swGetJSON.ElapsedMilliseconds + swJSONToObjects.ElapsedMilliseconds + swTaskOneTwo.ElapsedMilliseconds);


                // Build task three
                Stopwatch swTaskThree = new Stopwatch();
                swTaskThree.Start();

                StringBuilder sb2 = new StringBuilder();
                foreach (Person person in persons)
                {
                    sb2.AppendFormat("Full Name:  {0}", person.FullName);
                    sb2.AppendFormat("\nRegion:     {0}", person.Region);
                    sb2.AppendFormat("\n***** Credit Card *****");
                    sb2.AppendFormat("\nExpiration: {0}", person.CreditCard.Expiration);
                    sb2.AppendFormat("\nNumber:     {0}", person.CreditCard.Number);
                    sb2.AppendFormat("\nPin:        {0}", person.CreditCard.Pin);
                    sb2.AppendFormat("\nSecurity:   {0}", person.CreditCard.Security);
                    sb2.AppendFormat("\n\n***********************************\n\n");
                }

                // Display task three
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("################# Task Three ##################\n");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(sb2);

                swTaskThree.Stop();

                Console.WriteLine("Time to get the JSON: {0}ms", swGetJSON.ElapsedMilliseconds);
                Console.WriteLine("Time to Populate the objects from JSON: {0}ms", swJSONToObjects.ElapsedMilliseconds);
                Console.WriteLine("Time to build the string and display it: {0}ms", swTaskThree.ElapsedMilliseconds);
                Console.WriteLine("Combined Time: {0}ms\n\n", swGetJSON.ElapsedMilliseconds + swJSONToObjects.ElapsedMilliseconds + swTaskThree.ElapsedMilliseconds);

                //System.IO.File.WriteAllLines(@"c:\Users\drago\Desktop\test.txt", sb.ToString().Split('\n'));
            }

            Console.ReadLine();
        }

        static string GetJSONString(string path)
        {
            try
            {
                WebClient client = new WebClient
                {
                    Encoding = System.Text.Encoding.UTF8
                };
                return client.DownloadString(path);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n************************************************************\n");
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Path: '{0}'", path);
                Console.WriteLine("\n************************************************************\n");
                return "";
            }
        }

        static List<T> DeserializeJSONString<T>(string json)
        {
            try
            {
                return new JavaScriptSerializer().Deserialize<List<T>>(json);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n************************************************************\n");
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("JSON String:\n{0}", json);
                Console.WriteLine("\n************************************************************\n");
                return new List<T>();
            }
        }

    }

    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return char.ToUpper(this.Title[0]) + this.Title.Substring(1) + ". " + this.Name + " " + this.Surname; } }
        public string Gender { get; set; }
        public string Region { get; set; }
        public int Age { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public Birthday Birthday { get; set; }
        public CreditCard CreditCard { get; set; }
        // Convert api naming convention to application naming convention
        public CreditCard Credit_card { set { CreditCard = value; } }
    }

    class Birthday
    {
        public string Dmy { get; set; }
        public string Mdy { get; set; }
        public string Raw { get; set; }
    }

    class CreditCard
    {
        public string Expiration { get; set; }
        public string Number { get; set; }
        public int Pin { get; set; }
        public int Security { get; set; }
    }
}
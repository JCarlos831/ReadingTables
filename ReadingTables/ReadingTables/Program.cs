using System;
using OpenQA.Selenium.Firefox;

namespace ReadingTables
{
    class Program : Base
    {

        public static void Main(string[] args)
        {
            Driver = new FirefoxDriver(@"/Users/JuanCMontoya/Projects/vscode/csharp/ReadingTables/ReadingTables/ReadingTables/bin/Debug");

            // Need to insert an actual html table
            Driver.Navigate().GoToUrl("file:///C:/TablePages/ComplexTable.html");

            TablePage page = new TablePage();

            // Read Table
            Utilities.ReadTable(page.Table);

            Console.WriteLine("*****************************************************************");

            // Get the cell value from the table
            Console.WriteLine("The name {0} with email {1} and phone {2}", Utilities.ReadCell("Name", 2), Utilities.ReadCell("Email", 2), Utilities.ReadCell("Phone", 2));

            Console.WriteLine("*****************************************************************");

            // Delete Prashanth
            Utilities.PerformActionOnCell("5", "Name", "Prashanth", "Delete");

            Console.Read();
        }
    }
}

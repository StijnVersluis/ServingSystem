using LogicLayer;
using DataLayer;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TableContainer tCon = new TableContainer(new TableDAL());

            tCon.GetAllSeatedTables().ForEach(x => Console.WriteLine(x.Id + " => " + x.Name));
        }
    }
}

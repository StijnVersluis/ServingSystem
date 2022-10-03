using BusinessLayer;
using DataLayer;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var prod = new Product(new ProductDAL(), 1, "Test", 2.1, 1);

            Console.WriteLine(prod.Delete());
        }
    }
}

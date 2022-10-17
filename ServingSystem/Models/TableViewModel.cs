using DataLayer;
using LogicLayer;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ServingSystem.Models
{
    public class TableViewModel
    {
        public int Id { private set; get; }
        [Required]
        public string Name { private set; get; }
        public DateTime Time_Arrived { private set; get; }

        public TableViewModel(string name)
        {
            Name = name;
        }
        public TableViewModel(Table table)
        {
            Id = table.Id;
            Name = table.Name;
        }
        public TableViewModel(Table table, DateTime timearrived)
        {
            Id = table.Id;
            Name = table.Name;
            Time_Arrived = timearrived;
        }

        public double GetTotalPrice()
        {
            Table table = new Table(Id, Name);
            return table.GetTotalPrice(new TableDAL());
        }

        public string GetLastOrderText()
        {
            Table table = new Table(Id, Name);
            return table.GetLastOrderText(new TableDAL());
        }
    }
}

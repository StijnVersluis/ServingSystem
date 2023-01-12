using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.DTO
{
    public class TableDTO
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public DateTime Time_Arrived { set; get; }
        
        public TableDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public TableDTO(int id, string name, DateTime time_arrived)
        {
            Id = id;
            Name = name;
            Time_Arrived = time_arrived;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.DTO
{
    public class OrderDTO
    {
        public int Id;
        public int SeatedTableId;
        public int StaffId;
        public DateTime CreatedAt;

        public OrderDTO(int id, int seatedTable, int staffId, DateTime createdAt)
        {
            Id = id;
            SeatedTableId = seatedTable;
            StaffId = staffId;
            CreatedAt = createdAt;
        }
    }
}

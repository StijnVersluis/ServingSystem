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
        public List<ProductDTO> Products;
        public DateTime CreatedAt;


    }
}

using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductType(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public ProductType(ProductTypeDTO typeDTO)
        {
            Id = typeDTO.Id;
            Name = typeDTO.Name;
        }
    }
}

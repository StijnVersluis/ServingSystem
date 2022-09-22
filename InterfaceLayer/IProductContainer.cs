﻿using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface IProductContainer
    {
        public ProductDTO GetProduct(int Id);
        public List<ProductDTO> GetAll();
        public List<ProductDTO> GetAllOfType(int type);
    }
}

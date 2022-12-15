﻿using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface IProduct
    {
        public string GetProductTypeName(int typeId);
        public bool Edit(ProductDTO productDTO);
    }
}

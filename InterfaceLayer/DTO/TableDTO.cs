﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.DTO
{
    public class TableDTO
    {
        public int Id { private set; get; }
        public string Name { private set; get; }
        
        public TableDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public double GetPrice()
        {
            throw new NotImplementedException();
        }
    }
}

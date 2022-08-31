using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;

namespace InterfaceLayer
{
    public interface ITableContainer
    {
        public List<TableDTO> GetAll();
        public TableDTO GetTable(int id);
    }
}

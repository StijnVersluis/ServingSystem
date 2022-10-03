using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;

namespace InterfaceLayer
{
    public interface ITableContainer
    {
        public List<TableDTO> GetAll();
        public List<TableDTO> GetAllSeatedTables();
        public TableDTO GetTable(int id);
        public bool CreateTable(TableDTO table);
        public bool DeleteTable(int id);
    }
}

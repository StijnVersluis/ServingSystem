using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;

namespace InterfaceLayer
{
    public interface ITableContainer
    {
        public List<TableDTO> GetAll();
        public List<TableDTO> GetAllSeatedTables();
        public List<TableDTO> GetAllNonSeatedTables();
        public TableDTO GetTable(int id);
        public bool CreateTable(string name);
        public bool DeleteTable(int id);
        public bool OpenTable(int id);
        public bool CloseTable(int id);
    }
}

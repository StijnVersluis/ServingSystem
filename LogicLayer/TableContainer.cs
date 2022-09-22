using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TableContainer
    {
        private ITableContainer iTableContainer;
        public TableContainer(ITableContainer dal)
        {
            iTableContainer = dal;
        }
        public List<Table> GetAll()
        {
            return iTableContainer.GetAll().ConvertAll(x=>new Table(x));
        }

        public Table GetTable(int id)
        {
            return new Table(iTableContainer.GetTable(id));
        }
    }
}

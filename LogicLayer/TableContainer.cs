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
            var li = new List<Table>();
            iTableContainer.GetAll().ForEach(table => li.Add(new Table(table)));
            return li;
        }

        public Table GetTable(int id)
        {
            return new Table(iTableContainer.GetTable(id));
        }
    }
}

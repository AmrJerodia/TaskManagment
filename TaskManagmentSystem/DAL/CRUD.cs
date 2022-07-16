using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CRUD
    {
        private static SqlDb _db;

        protected CRUD()
        {
            if (_db == null)
            {
                _db = new SqlDb();
            }
        }

        public SqlDb GetDb()
        {
            return _db;
        }
    }
}

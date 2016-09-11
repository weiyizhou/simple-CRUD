using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBEntityFactory
{
    public class DBEntityFactory
    {
        public enum dbefFlag
        {
            TestSuite
        };
        public IDBEntity.IDBTestSuite DBConnection(dbefFlag flag)
        {
            //if (flag == dbefFlag.TestSuite)
            //{
            IDBEntity.IDBTestSuite newTestSuiteEntity = new SQLite.DBEntity.TestSuiteEntry();
            return newTestSuiteEntity;
            //}
        }
    }
}

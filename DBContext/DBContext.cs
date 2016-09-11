using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace DBContext
{
    public class DBContext : IQuery.IQuery
    {
        public enum dbType
        {
            SQLite
        };
        public enum dbefFlag
        {
            TestSuite,
            TestSet,
            TestCase
        };
        public IConnection.IConnection DBConnection(dbType flag, string dbPath)
        {
            //if (flag == dbFlag.SQLite)
            //{
            IConnection.IConnection newSQLiteConnection = new SQLite.SQLiteConnection(dbPath, true);
            return newSQLiteConnection;
            //}
        }
        public string PulledFilesQuery(string fileName, string path)
        {
            SQLite.SQLiteConnection link = new SQLite.SQLiteConnection(path, true);
            string[] args = new string[1] { fileName };
            DBEntity.PulledFilesEntry results = new DBEntity.PulledFilesEntry();
            results = (link.Query<DBEntity.PulledFilesEntry>("SELECT * FROM pulledFiles WHERE ? = fileName", args)).FirstOrDefault();
            if (results == null)
            {
                return null;
            }
            return results.fileName;
        }
        public List<IQuery.ITestSuiteEntity> DBSuiteQuery(string dbPath)
        {
            SQLite.SQLiteConnection link = new SQLite.SQLiteConnection(dbPath, true);
            List<IQuery.ITestSuiteEntity> results = new List<IQuery.ITestSuiteEntity>();
            List<SQLite.DBEntity.TestSuiteEntry> temp = link.Query<SQLite.DBEntity.TestSuiteEntry>("SELECT * FROM testSuite");
            foreach (SQLite.DBEntity.TestSuiteEntry tse in temp)
            {
                results.Add(tse);
            }
            return results;
        }
        public List<IQuery.ITestSuiteEntity> DBSuiteQuery(string suiteKey, string dbPath)
        {
            SQLite.SQLiteConnection link = new SQLite.SQLiteConnection(dbPath, true);
            List<IQuery.ITestSuiteEntity> results = new List<IQuery.ITestSuiteEntity>();
            string[] args = new string[1] { suiteKey };
            List<SQLite.DBEntity.TestSuiteEntry> temp = link.Query<SQLite.DBEntity.TestSuiteEntry>("SELECT * FROM testSuite WHERE testSuiteKey = ?", args);
            foreach (SQLite.DBEntity.TestSuiteEntry tse in temp)
            {
                results.Add(tse);
            }
            return results;
        }
        public List<IQuery.ITestSetEntity> DBSetQuery(string dbPath)
        {
            SQLite.SQLiteConnection link = new SQLite.SQLiteConnection(dbPath, true);
            List<IQuery.ITestSetEntity> results = new List<IQuery.ITestSetEntity>();
            List<SQLite.DBEntity.TestSetEntry> temp = link.Query<SQLite.DBEntity.TestSetEntry>("SELECT * FROM testSet");
            foreach (SQLite.DBEntity.TestSetEntry tste in temp)
            {
                results.Add(tste);
            }
            return results;
        }
        public List<IQuery.ITestSetEntity> DBSetQuery(string setKey, string dbPath)
        {
            SQLite.SQLiteConnection link = new SQLite.SQLiteConnection(dbPath, true);
            List<IQuery.ITestSetEntity> results = new List<IQuery.ITestSetEntity>();
            string[] args = new string[1] { setKey };
            List<SQLite.DBEntity.TestSetEntry> temp = link.Query<SQLite.DBEntity.TestSetEntry>("SELECT * FROM testSet WHERE testSetKey = ?", args);
            foreach (SQLite.DBEntity.TestSetEntry tste in temp)
            {
                results.Add(tste);
            }
            return results;
        }
        public List<IQuery.ITestSetEntity> DBSuiteSetQuery(string suiteKey, string dbPath)
        {
            SQLite.SQLiteConnection link = new SQLite.SQLiteConnection(dbPath, true);
            List<IQuery.ITestSetEntity> results = new List<IQuery.ITestSetEntity>();
            string[] args = new string[1] { suiteKey };
            List<SQLite.DBEntity.TestSetEntry> temp = link.Query<SQLite.DBEntity.TestSetEntry>("SELECT * FROM testSet WHERE testSuiteKey = ?", args);
            foreach (SQLite.DBEntity.TestSetEntry tste in temp)
            {
                results.Add(tste);
            }
            return results;
        }
        public List<IQuery.ITestCaseEntity> DBCaseQuery(string dbPath)
        {
            SQLite.SQLiteConnection link = new SQLite.SQLiteConnection(dbPath, true);
            List<IQuery.ITestCaseEntity> results = new List<IQuery.ITestCaseEntity>();
            List<SQLite.DBEntity.TestCaseEntry> temp = link.Query<SQLite.DBEntity.TestCaseEntry>("SELECT * FROM testCase");
            foreach (SQLite.DBEntity.TestCaseEntry tce in temp)
            {
                results.Add(tce);
            }
            return results;
        }
        public List<IQuery.ITestCaseEntity> DBCaseQuery(string caseKey, string dbPath)
        {
            SQLite.SQLiteConnection link = new SQLite.SQLiteConnection(dbPath, true);
            List<IQuery.ITestCaseEntity> results = new List<IQuery.ITestCaseEntity>();
            string[] args = new string[1] { caseKey };
            List<SQLite.DBEntity.TestCaseEntry> temp = link.Query<SQLite.DBEntity.TestCaseEntry>("SELECT * FROM testCase WHERE testCaseKey = ?", args);
            foreach (SQLite.DBEntity.TestCaseEntry tce in temp)
            {
                results.Add(tce);
            }
            return results;
        }
        public List<IQuery.ITestCaseEntity> DBSetCaseQuery(string setKey, string dbPath)
        {
            SQLite.SQLiteConnection link = new SQLite.SQLiteConnection(dbPath, true);
            List<IQuery.ITestCaseEntity> results = new List<IQuery.ITestCaseEntity>();
            string[] args = new string[1] { setKey };
            List<SQLite.DBEntity.TestCaseEntry> temp = link.Query<SQLite.DBEntity.TestCaseEntry>("SELECT * FROM testCase WHERE testSetKey = ?", args);
            foreach (SQLite.DBEntity.TestCaseEntry tce in temp)
            {
                results.Add(tce);
            }
            return results;
        }
    }
}

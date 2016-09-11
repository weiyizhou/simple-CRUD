using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using IDataRepo;
using IConnection;
using DBContext;


namespace DataRepo
{
    public class DataRepo : IDataRepo.IDataRepo
    {
        private readonly string dbPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "MyDatabase.db");
        private readonly string pulledFiles = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "pulledFiles.db");

        public bool CreateTestDB() 
        {
            if (!File.Exists(dbPath))
            {
                DBContext.DBContext temp = new DBContext.DBContext();
                IConnection.IConnection dbConnection = temp.DBConnection(DBContext.DBContext.dbType.SQLite, dbPath);
                string query = "PRAGMA foreign_keys = ON";
                dbConnection.Execute(query);
                //If desired, insert additional parameters in query below to have them added to the SQL database
                query = "CREATE TABLE testSuite(testSuiteKey TEXT PRIMARY KEY NOT NULL, runTime TEXT NOT NULL, rtYear INTEGER NOT NULL, rtMonth INTEGER NOT NULL, rtDay INTEGER NOT NULL, rtHour INTEGER NOT NULL, rtMinute INTEGER NOT NULL, lastModified TEXT NOT NULL, lmYear INTEGER NOT NULL, lmMonth INTEGER NOT NULL, lmDay INTEGER NOT NULL, lmHour INTEGER NOT NULL, lmMinute INTEGER NOT NULL)";
                dbConnection.Execute(query);
                //If desired, insert additional parameters in query below to have them added to the SQL database
                query = "CREATE TABLE testSet(testSuiteKey TEXT NOT NULL, testSetKey TEXT PRIMARY KEY NOT NULL, runTime TEXT NOT NULL, rtYear INTEGER NOT NULL, rtMonth INTEGER NOT NULL, rtDay INTEGER NOT NULL, rtHour INTEGER NOT NULL, rtMinute INTEGER NOT NULL, lastModified TEXT NOT NULL, lmYear INTEGER NOT NULL, lmMonth INTEGER NOT NULL, lmDay INTEGER NOT NULL, lmHour INTEGER NOT NULL, lmMinute INTEGER NOT NULL, FOREIGN KEY(testSuiteKey) REFERENCES testSuite(testSuiteKey) ON DELETE CASCADE ON UPDATE CASCADE)";
                dbConnection.Execute(query);
                //If desired, insert additional parameters in query below to have them added to the SQL database
                query = "CREATE TABLE testCase(testSetKey TEXT NOT NULL, testCaseKey TEXT PRIMARY KEY NOT NULL, runTime TEXT NOT NULL, rtYear INTEGER NOT NULL, rtMonth INTEGER NOT NULL, rtDay INTEGER NOT NULL, rtHour INTEGER NOT NULL, rtMinute INTEGER NOT NULL, lastModified TEXT NOT NULL, lmYear INTEGER NOT NULL, lmMonth INTEGER NOT NULL, lmDay INTEGER NOT NULL, lmHour INTEGER NOT NULL, lmMinute INTEGER NOT NULL, FOREIGN KEY(testSetKey) REFERENCES testSet(testSetKey) ON DELETE CASCADE ON UPDATE CASCADE)";
                dbConnection.Execute(query);
                dbConnection.Close();
                return true;
            }
            return false;
        }
        //If additional parameters were added when the database was created, insert them into the input parameters of this function and set them accordingly
        public string EnterTestSuite(string testSuiteKey, DateTime runTime, int rtYear, int rtMonth, int rtDay, int rtHour, int rtMinute)
        {
            DBContext.DBContext temp = new DBContext.DBContext();
            IConnection.IConnection dbConnection = temp.DBConnection(DBContext.DBContext.dbType.SQLite, dbPath);
            string query = "PRAGMA foreign_keys = ON";
            dbConnection.Execute(query);
            // If additional parameters were added to the input parameters, add them to the INSERT INTO statement and add the corresponding amount of question marks to the VALUES section
            query = "INSERT INTO testSuite (testSuiteKey, runTime, rtYear, rtMonth, rtDay, rtHour, rtMinute, lastModified, lmYear, lmMonth, lmDay, lmHour, lmMinute) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            DateTime lastModified = DateTime.Now;
            int lmYear = lastModified.Year;
            int lmMonth = lastModified.Month;
            int lmDay = lastModified.Day;
            int lmHour = lastModified.Hour;
            int lmMinute = lastModified.Minute;
            // If additional parameters were added to the input parameters, add them to the statement below and adjust the number in the brackets accordingly
            object[] arguments = new object[13] { testSuiteKey, runTime, rtYear, rtMonth, rtDay, rtHour, rtMinute, lastModified, lmYear, lmMonth, lmDay, lmHour, lmMinute};
            dbConnection.Execute(query, arguments);
            dbConnection.Close();
            return testSuiteKey;

        }
        //If additional parameters were added when the database was created, insert them into the input parameters of this function and set them accordingly
        public string EnterTestSet(string testSuiteKey, string testSetKey, string testSetName, int passTotal, int failTotal, int setTotal, string setMessages, float runDuration, DateTime runTime, int rtYear, int rtMonth, int rtDay, int rtHour, int rtMinute)
        {
            DBContext.DBContext temp = new DBContext.DBContext();
            IConnection.IConnection dbConnection = temp.DBConnection(DBContext.DBContext.dbType.SQLite, dbPath);
            string query = "PRAGMA foreign_keys = ON";
            dbConnection.Execute(query);
            // If additional parameters were added to the input parameters, add them to the INSERT INTO statement and add the corresponding amount of question marks to the VALUES section
            query = "INSERT INTO testSet (testSuiteKey, testSetKey, testSetName, runTime, rtYear, rtMonth, rtDay, rtHour, rtMinute, lastModified, lmYear, lmMonth, lmDay, lmHour, lmMinute) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            DateTime lastModified = DateTime.Now;
            int lmYear = lastModified.Year;
            int lmMonth = lastModified.Month;
            int lmDay = lastModified.Day;
            int lmHour = lastModified.Hour;
            int lmMinute = lastModified.Minute;
            // If additional parameters were added to the input parameters, add them to the statement below and adjust the number in the brackets accordingly
            object[] arguments = new object[15] { testSuiteKey, testSetKey, testSetName, runTime, rtYear, rtMonth, rtDay, rtHour, rtMinute, lastModified, lmYear, lmMonth, lmDay, lmHour, lmMinute };
            dbConnection.Execute(query, arguments);
            dbConnection.Close();
            return testSetKey;
        }
        //If additional parameters were added when the database was created, insert them into the input parameters of this function and set them accordingly
        public string EnterTestCase(string testSetKey, string testCaseKey, string testCaseName,  DateTime runTime, int rtYear, int rtMonth, int rtDay, int rtHour, int rtMinute)
        {
            DBContext.DBContext temp = new DBContext.DBContext();
            IConnection.IConnection dbConnection = temp.DBConnection(DBContext.DBContext.dbType.SQLite, dbPath);
            string query = "PRAGMA foreign_keys = ON";
            dbConnection.Execute(query);
            // If additional parameters were added to the input parameters, add them to the INSERT INTO statement and add the corresponding amount of question marks to the VALUES section
            query = "INSERT INTO testCase(testSetKey, testCaseKey, testCaseName, rtYear, rtMonth, rtDay, rtHour, rtMinute, lastModified, lmYear, lmMonth, lmDay, lmHour, lmMinute) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            DateTime lastModified = DateTime.Now;
            int lmYear = lastModified.Year;
            int lmMonth = lastModified.Month;
            int lmDay = lastModified.Day;
            int lmHour = lastModified.Hour;
            int lmMinute = lastModified.Minute;
            // If additional parameters were added to the input parameters, add them to the statement below and adjust the number in the brackets accordingly
            object[] arguments = new object[14] { testSetKey, testCaseKey, testCaseName, rtYear, rtMonth, rtDay, rtHour, rtMinute, lastModified, lmYear, lmMonth, lmDay, lmHour, lmMinute };
            dbConnection.Execute(query, arguments);
            dbConnection.Close();
            return testCaseKey;
        }
        public string RetrieveEntry(int table)
        {
            {
                string temp;
                string result;
                IQuery.IQuery dbConnection = new DBContext.DBContext();
                if (table == 0)
                {
                    //Enter additional bar delimited labels below if additional parameters were added earlier
                    result = "Test Suite Runs\ntestSuiteKey | runTime | lastModified\n";
                    List<IQuery.ITestSuiteEntity> results = dbConnection.DBSuiteQuery(dbPath);
                    foreach (IQuery.ITestSuiteEntity tse in results)
                    {
                        temp = String.Format("{0} | {1} | {2} | {3}\n", tse.testSuiteKey, tse.runTime, tse.lastModified);
                        result = String.Concat(result, temp);
                    }
                }
                else if (table == 1)
                {
                    //TODO: DOCUMENT EVERYTHING FROM HERE ON DOWN
                    result = "Test Set Runs\ntestSuiteKey | testSetKey | testSetName | runTime | lastModified\n";
                    List<IQuery.ITestSetEntity> results = dbConnection.DBSetQuery(dbPath);
                    foreach (IQuery.ITestSetEntity tste in results)
                    {
                        temp = String.Format("{0} | {1} | {2} | {3} | {4} | {5} \n", tste.testSuiteKey, tste.testSetKey, tste.testSetName, tste.runTime, tste.lastModified);
                        result = String.Concat(result, temp);
                    }
                }
                else if (table == 2)
                {
                    result = "Test Case Runs\ntestSetKey | testCaseKey | testCaseName |runTime | lastModified\n";
                    List<IQuery.ITestCaseEntity> results = dbConnection.DBCaseQuery(dbPath);
                    foreach (IQuery.ITestCaseEntity tce in results)
                    {
                        temp = String.Format("{0} | {1} | {2} | {3} | {4} | {5}\n", tce.testSetKey, tce.testCaseKey, tce.testCaseName, tce.runTime, tce.lastModified);
                        result = String.Concat(result, temp);
                    }
                }
                else
                {
                    result = "ERROR: Invalid table\n";
                }
                return result;
            }
        }
        public string RetrieveEntry(int table, string entryKey)
        {
            string temp;
            string result;
            IQuery.IQuery dbConnection = new DBContext.DBContext();
            if (table == 0)
            {
                result = "Matching Test Suite Runs\ntestSuiteKey | runTime | lastModified\n";
                List<IQuery.ITestSuiteEntity> results = dbConnection.DBSuiteQuery(entryKey, dbPath);
                foreach (IQuery.ITestSuiteEntity tse in results)
                {
                    temp = String.Format("{0} | {1} | {2} | {3}\n", tse.testSuiteKey, tse.runTime, tse.lastModified);
                    result = String.Concat(result, temp);
                }
                foreach (IQuery.ITestSuiteEntity tse in results)
                {
                    result = String.Concat(result, RetrieveEntryHelper(1, tse.testSuiteKey));
                }
            }
            else if (table == 1)
            {
                result = "Matching Test SetRuns\ntestSuiteKey | testSetKey | testSetName | runTime | lastModified\n";
                List<IQuery.ITestSetEntity> results = dbConnection.DBSetQuery(entryKey, dbPath);
                foreach (IQuery.ITestSetEntity tste in results)
                {
                    temp = String.Format("{0} | {1} | {2} | {3} | {4} | {5} \n", tste.testSuiteKey, tste.testSetKey, tste.testSetName, tste.runTime, tste.lastModified);
                    result = String.Concat(result, temp);
                }
                foreach (IQuery.ITestSetEntity tste in results)
                {
                    result = String.Concat(result, RetrieveEntryHelper(2, tste.testSetKey));
                }
            }
            else if (table == 2)
            {
                result = "Matching Test Case Runs\ntestSetKey | testCaseKey | testCaseName |runTime | lastModified\n";
                List<IQuery.ITestCaseEntity> results = dbConnection.DBCaseQuery(entryKey, dbPath);
                foreach (IQuery.ITestCaseEntity tce in results)
                {
                    temp = String.Format("{0} | {1} | {2} | {3} | {4} | {5}\n", tce.testSetKey, tce.testCaseKey, tce.testCaseName, tce.runTime, tce.lastModified);
                    result = String.Concat(result, temp);
                }
            }
            else
            {
                result = "ERROR: Invalid table";
            }
            return result;
        }
        public string RetrieveEntryHelper(int table, string entryKey)
        {
            string temp;
            string result;
            IQuery.IQuery dbConnection = new DBContext.DBContext();
            if (table == 1)
            {
                result = "Matching Test Set Runs\ntestSuiteKey | testSetKey | testSetName | runTime | lastModified\n";
                List<IQuery.ITestSetEntity> results = dbConnection.DBSuiteSetQuery(entryKey, dbPath);
                foreach (IQuery.ITestSetEntity tste in results)
                {
                    temp = String.Format("{0} | {1} | {2} | {3} | {4} | {5} \n", tste.testSuiteKey, tste.testSetKey, tste.testSetName, tste.runTime, tste.lastModified);
                    result = String.Concat(result, temp);
                }
                foreach (IQuery.ITestSetEntity tste in results)
                {
                    result = String.Concat(result, RetrieveEntryHelper(2, tste.testSetKey));
                }
            }
            else if (table == 2)
            {
                result = "Matching Test Case Runs\ntestSetKey | testCaseKey | testCaseName |runTime | lastModified\n";
                List<IQuery.ITestCaseEntity> results = dbConnection.DBSetCaseQuery(entryKey, dbPath);
                foreach (IQuery.ITestCaseEntity tce in results)
                {
                    temp = String.Format("{0} | {1} | {2} | {3} | {4} | {5}\n", tce.testSetKey, tce.testCaseKey, tce.testCaseName, tce.runTime, tce.lastModified);
                    result = String.Concat(result, temp);
                }
            }
            else
            {
                result = "ERROR: Invalid table";
            }
            return result;
        }
        public void DeleteEntry(string tableName, string entryKey)
        {
            DBContext.DBContext temp = new DBContext.DBContext();
            IConnection.IConnection dbConnection = temp.DBConnection(DBContext.DBContext.dbType.SQLite, dbPath);
            string query = "PRAGMA foreign_keys = ON";
            dbConnection.Execute(query);
            if (string.Equals(tableName, "testSuite"))
            {
                query = "DELETE FROM testSuite WHERE testSuiteKey = ?";
            }
            else if (string.Equals(tableName, "testSet"))
            {
                query = "DELETE FROM testSet WHERE testSetKey = ?";
            }
            else if (string.Equals(tableName, "testCase"))
            {
                query = "DELETE FROM testCase WHERE testCaseKey = ?";
            }
            string[] arguments = new string[1] { entryKey };
            dbConnection.Execute(query, arguments);
            dbConnection.Close();
        }
        public void DeleteTestDB()
        {
            DBContext.DBContext temp = new DBContext.DBContext();
            IConnection.IConnection dbConnection = temp.DBConnection(DBContext.DBContext.dbType.SQLite, dbPath);
            string query = "PRAGMA foreign_keys = ON";
            dbConnection.Execute(query);
            query = "DROP TABLE testSuite";
            dbConnection.Execute(query);
            query = "DROP TABLE testSet";
            dbConnection.Execute(query);
            query = "DROP TABLE testCase";
            dbConnection.Execute(query);
            dbConnection.Close();
        }
        public void CreatePFDB()
        {
            if (!File.Exists(pulledFiles))
            {
                DBContext.DBContext temp = new DBContext.DBContext();
                IConnection.IConnection dbConnection = temp.DBConnection(DBContext.DBContext.dbType.SQLite, pulledFiles);
                string query = "CREATE TABLE pulledFiles(fileName TEXT PRIMARY KEY NOT NULL, time TEXT NOT NULL, year INTEGER NOT NULL, month INTEGER NOT NULL, day INTEGER NOT NULL, hour INTEGER NOT NULL, minute INTEGER NOT NULL)";
                dbConnection.Execute(query);
                dbConnection.Close();
            }
        }
        public void InsertPF(string fileName, DateTime time, int year, int month, int day, int hour, int minute)
        {
            DBContext.DBContext temp = new DBContext.DBContext();
            IConnection.IConnection dbConnection = temp.DBConnection(DBContext.DBContext.dbType.SQLite, pulledFiles);
            string query = "INSERT INTO pulledFiles(fileName, time, year, month, day, hour, minute) VALUES (?, ?, ?, ?, ?, ?, ?)";
            object[] arguments = new object[7] { fileName, time, year, month, day, hour, minute };
            dbConnection.Execute(query, arguments);
            dbConnection.Close();
        }
        public string PullFile(string fileName)
        {
            IQuery.IQuery query = new DBContext.DBContext();
            return query.PulledFilesQuery(fileName, pulledFiles);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataRepo
{
    public interface IDataRepo
    {
        bool CreateTestDB();
        //Match these method parameters if changes were made in the DataRepo class
        string EnterTestSuite(string testSuiteKey, int benchNumber, int passedTestSets, float runDuration, DateTime runTime, int rtYear, int rtMonth, int rtDay, int rtHour, int rtMinute);
        string EnterTestSet(string testSuiteKey, string testSetKey, string testSetName, int passTotal, int failTotal, int setTotal, string setMessages, float runDuration, DateTime runTime, int rtYear, int rtMonth, int rtDay, int rtHour, int rtMinute);
        string EnterTestCase(string testSetKey, string testCaseKey, string testCaseName, string result, string message, DateTime runTime, int rtYear, int rtMonth, int rtDay, int rtHour, int rtMinute);
        string RetrieveEntry(int table);
        string RetrieveEntry(int table, string entryKey);
        void DeleteEntry(string tableName, string entryKey);
        void DeleteTestDB();
        void CreatePFDB();
        void InsertPF(string fileName, DateTime time, int year, int month, int day, int hour, int minute);
        string PullFile(string fileName);
    }
}

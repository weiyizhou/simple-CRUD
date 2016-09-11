using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQuery
{
    public interface IQuery
    {
        List<ITestSuiteEntity> DBSuiteQuery(string dbPath);
        List<ITestSuiteEntity> DBSuiteQuery(string suiteKey, string dbPath);
        List<ITestSetEntity> DBSetQuery(string dbPath);
        List<ITestSetEntity> DBSetQuery(string setKey, string dbPath);
        List<ITestSetEntity> DBSuiteSetQuery(string suiteKey, string dbPath);
        List<ITestCaseEntity> DBCaseQuery(string dbPath);
        List<ITestCaseEntity> DBCaseQuery(string caseKey, string dbPath);
        List<ITestCaseEntity> DBSetCaseQuery(string setKey, string dbPath);
        string PulledFilesQuery(string fileName, string path);     
    }
    public interface ITestSuiteEntity
    {
        string testSuiteKey { get; set; }
        /*
         * INSERT ADDITIONAL PARAMETERS HERE
         */
        DateTime runTime { get; set; }
        int rtYear { get; set; }
        int rtMonth { get; set; }
        int rtDay { get; set; }
        int rtHour { get; set; }
        int rtMinute { get; set; }
        DateTime lastModified { get; set; }
        int lmYear { get; set; }
        int lmMonth { get; set; }
        int lmDay { get; set; }
        int lmHour { get; set; }
        int lmMinute { get; set; }
    }
    public interface ITestSetEntity
    {
        string testSuiteKey { get; set; }
        string testSetKey { get; set; }
        string testSetName { get; set; }
        /*
         *
         * INSERT ADDITIONAL PARAMETERS HERE
         * 
         */
        DateTime runTime { get; set; }
        int rtYear { get; set; }
        int rtMonth { get; set; }
        int rtDay { get; set; }
        int rtHour { get; set; }
        int rtMinute { get; set; }
        DateTime lastModified { get; set; }
        int lmYear { get; set; }
        int lmMonth { get; set; }
        int lmDay { get; set; }
        int lmHour { get; set; }
        int lmMinute { get; set; }

    }
    public interface ITestCaseEntity
    {
        string testSetKey { get; set; }
        string testCaseKey { get; set; }
        string testCaseName{ get; set; }
        /*
         *
         * INSERT ADDITIONAL PARAMETERS HERE
         * 
         */
        DateTime runTime { get; set; }
        int rtYear { get; set; }
        int rtMonth { get; set; }
        int rtDay { get; set; }
        int rtHour { get; set; }
        int rtMinute { get; set; }
        DateTime lastModified { get; set; }
        int lmYear { get; set; }
        int lmMonth { get; set; }
        int lmDay { get; set; }
        int lmHour { get; set; }
        int lmMinute { get; set; }
    }
    public interface IPulledFilesEntry
    {
        string fileName { get; set; }
        string time { get; set; }
        int year { get; set; }
        int month { get; set; }
        int day { get; set; }
        int hour { get; set; }
        int minute { get; set; }
    }

}

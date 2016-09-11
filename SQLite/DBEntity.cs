using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLite
{
    public class DBEntity 
    {
        public class TestSuiteEntry : IQuery.ITestSuiteEntity
        {
            [PrimaryKey, NotNull]
            public string testSuiteKey { get; set; }
            /*
             *
             * INSERT ADDITIONAL PARAMETERS HERE
             * 
             */
            public DateTime runTime { get; set; }
            [NotNull]
            public int rtYear { get; set; }
            [NotNull]
            public int rtMonth { get; set; }
            [NotNull]
            public int rtDay { get; set; }
            [NotNull]
            public int rtHour { get; set; }
            [NotNull]
            public int rtMinute { get; set; }
            [NotNull]
            public DateTime lastModified { get; set; }
            [NotNull]
            public int lmYear { get; set; }
            [NotNull]
            public int lmMonth { get; set; }
            [NotNull]
            public int lmDay { get; set; }
            [NotNull]
            public int lmHour { get; set; }
            [NotNull]
            public int lmMinute { get; set; }

        }
        public class TestSetEntry : IQuery.ITestSetEntity
        {
            [NotNull]
            public string testSuiteKey { get; set; }
            [PrimaryKey, NotNull]
            public string testSetKey { get; set; }
            [NotNull]
            public string testSetName { get; set; }
            /*
             *
             * INSERT ADDITIONAL PARAMETERS HERE
             * 
             */
            public DateTime runTime { get; set; }
            [NotNull]
            public int rtYear { get; set; }
            [NotNull]
            public int rtMonth { get; set; }
            [NotNull]
            public int rtDay { get; set; }
            [NotNull]
            public int rtHour { get; set; }
            [NotNull]
            public int rtMinute { get; set; }
            [NotNull]
            public DateTime lastModified { get; set; }
            [NotNull]
            public int lmYear { get; set; }
            [NotNull]
            public int lmMonth { get; set; }
            [NotNull]
            public int lmDay { get; set; }
            [NotNull]
            public int lmHour { get; set; }
            [NotNull]
            public int lmMinute { get; set; }

        }
        public class TestCaseEntry : IQuery.ITestCaseEntity
        {
            [NotNull]
            public string testSetKey { get; set; }
            [PrimaryKey, NotNull]
            public string testCaseKey { get; set; }
            [NotNull]
            public string testCaseName { get; set; }
            [NotNull]
            /*
             *
             * INSERT ADDITIONAL PARAMETERS HERE
             * 
             */
            public DateTime runTime { get; set; }
            [NotNull]
            public int rtYear { get; set; }
            [NotNull]
            public int rtMonth { get; set; }
            [NotNull]
            public int rtDay { get; set; }
            [NotNull]
            public int rtHour { get; set; }
            [NotNull]
            public int rtMinute { get; set; }
            [NotNull]
            public DateTime lastModified { get; set; }
            [NotNull]
            public int lmYear { get; set; }
            [NotNull]
            public int lmMonth { get; set; }
            [NotNull]
            public int lmDay { get; set; }
            [NotNull]
            public int lmHour { get; set; }
            [NotNull]
            public int lmMinute { get; set; }

        }
        public class PulledFilesEntry : IQuery.IPulledFilesEntry
        {
            [PrimaryKey, NotNull]
            public string fileName { get; set; }
            [NotNull]
            public string time { get; set; }
            [NotNull]
            public int year { get; set; }
            [NotNull]
            public int month { get; set; }
            [NotNull]
            public int day { get; set; }
            [NotNull]
            public int hour { get; set; }
            [NotNull]
            public int minute { get; set; }
        }
    }
}

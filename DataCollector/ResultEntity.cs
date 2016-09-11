using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollector
{
    public class ResultEntity
    {
        public class TestSuiteEntry
        {
            public IDictionary<string, TestSetEntry> tSetDictionary { get; set; }
            public string testSuiteKey { get; set; }
            public int benchNumber { get; set; }
            public int passedTestSets { get; set; }
            public float runDuration { get; set; }
            public DateTime runTime { get; set; }
            public int rtYear { get; set; }
            public int rtMonth { get; set; }
            public int rtDay { get; set; }
            public int rtHour { get; set; }
            public int rtMinute { get; set; }
        }
        public class TestSetEntry
        {
            public IDictionary<string, TestCaseEntry> tCaseDictionary { get; set; }
            public string testSuiteKey { get; set; }
            public string testSetKey { get; set; }
            public string testSetName { get; set; }
            public int passTotal { get; set; }
            public int failTotal { get; set; }
            public int setTotal { get; set; }
            public string setMessages { get; set; }
            public float runDuration { get; set; }
            public DateTime runTime { get; set; }
            public int rtYear { get; set; }
            public int rtMonth { get; set; }
            public int rtDay { get; set; }
            public int rtHour { get; set; }
            public int rtMinute { get; set; }
        }
        public class TestCaseEntry
        {
            public string testSetKey { get; set; }
            public string testCaseKey { get; set; }
            public string testCaseName { get; set; }
            public string result { get; set; }
            public string message { get; set; }
            public DateTime runTime { get; set; }
            public int rtYear { get; set; }
            public int rtMonth { get; set; }
            public int rtDay { get; set; }
            public int rtHour { get; set; }
            public int rtMinute { get; set; }
        }
    }
}

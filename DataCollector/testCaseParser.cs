using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataCollector
{
    class testCaseParser
    {
        public void ParseTestCase(string line, string tcMessage, string tcName)
        {
            if (Regex.Match(line, @"^FAIL: ", RegexOptions.Multiline).Success)
            { //store fail condition
                tcMessage += line;
                tcName = Regex.Match(line, @"test_\d+_\w+", RegexOptions.Multiline).Value;
                line = file.ReadLine();
                do
                {
                    line = file.ReadLine();
                    tcMessage += line;

                }
                while (!Regex.Match(line, @"^AssertionError: ", RegexOptions.Multiline).Success);
                tsMessage += tcMessage;
                failedTC.Add(tcName, tcMessage);
                tcMessage = null;
            }
            if (Regex.Match(line, @"^(Ran )(\d+ )(tests in )((\d+).(\d+))(s)", RegexOptions.Multiline).Success)
            {
                newTS.tSetDictionary[setKey].setTotal += Int32.Parse(Regex.Match(line, @"\d+").Value);
                string temp = Regex.Match(line, @"((\d+\.\d+))").Value;
                newTS.tSetDictionary[setKey].runDuration = float.Parse(temp);
                newTS.runDuration += newTS.tSetDictionary[setKey].runDuration;
            }
            if (Regex.Match(line, @"failures=\d+", RegexOptions.Multiline).Success)
            {
                newTS.tSetDictionary[setKey].failTotal = Int32.Parse(Regex.Match(line, @"\d+", RegexOptions.Multiline).Value);
            }
            if (Regex.Match(line, @"^START:", RegexOptions.Multiline).Success)
            {
                tcMessage = line;
                tcName = Regex.Match(line, @"test_\d+_\w+", RegexOptions.Multiline).Value;
                do
                {
                    line = file.ReadLine();
                    if (line != null)
                    {
                        tcMessage += line;
                    }

                }
                while (!Regex.Match(line, @"^END:", RegexOptions.Multiline).Success);
                tsMessage += tcMessage;

                if (failedTC.ContainsKey(tcName))
                {
                    newTS.tSetDictionary[setKey].tCaseDictionary[caseKey] = new ResultEntity.TestCaseEntry { testCaseName = tcName, message = failedTC[tcName] };
                    newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].message += tcMessage;
                    newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].result = "F";
                }
                else
                {
                    newTS.tSetDictionary[setKey].tCaseDictionary[caseKey] = new ResultEntity.TestCaseEntry { testCaseName = tcName, message = tcMessage };
                    newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].result = "P";
                }
                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].testSetKey = newTS.tSetDictionary[setKey].testSetKey;
                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].testCaseKey = (Guid.NewGuid()).ToString();


            }
            if (Regex.Match(line, @"^Runtime: ", RegexOptions.Multiline).Success)
            {
                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime = Convert.ToDateTime(line.Substring(line.IndexOf(":") + 2));
                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].rtYear = (newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime).Year;
                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].rtMonth = (newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime).Month;
                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].rtDay = (newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime).Day;
                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].rtHour = (newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime).Hour;
                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].rtMinute = (newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime).Minute;
                setTime = newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime;
                caseCounter++;
            }
        }

    }
}

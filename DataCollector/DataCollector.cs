using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.IO;    

namespace DataCollector
{
    public class DataCollector
    {
        string fileName { get; set; }
        string target { get; set; }
        
        public DataCollector(string fileName, string target) {
            this.fileName = fileName;
            this.target = target;
        }

        public void parseAndInput()
        {
            StreamReader file;
            int btNum = Int32.Parse(Path.GetFileName(fileName).Split('_')[0].Substring(2)); //extracting BT Number from file name
            using (file = new StreamReader(Path.Combine(target, fileName))) //creating streamreader object form target
            {
                string line; //store line
                int setCounter = 0;
                int caseCounter = 0;
                string setKey = null;
                string caseKey = null;
                string tcName = null;
                DateTime suiteTime = new DateTime();
                ResultEntity.TestSuiteEntry newTS = new ResultEntity.TestSuiteEntry //creating new TestSuite
                { 
                    passedTestSets = 0,
                    tSetDictionary = new Dictionary<string, ResultEntity.TestSetEntry>(), //creating dictionary to store subordinate test sets
                    testSuiteKey = (Guid.NewGuid()).ToString() //generating test suite key
                };
                Dictionary<string, string> failedTC = new Dictionary<string, string>(); //create table to store testcases that failed
                while ((line = file.ReadLine()) != null) //outermost loop, keeps program reading through the file line by line until the end
                {
                    if (Regex.Match(line, @"^test_case_set: ", RegexOptions.Multiline).Success) //if the program reads "test_case_set:", this signals the begnning of a test set's data
                    {
                        setKey = "testSet" + setCounter.ToString(); //creating key in parent TestSuite dictionary to store TestSet in
                        newTS.tSetDictionary[setKey] = new ResultEntity.TestSetEntry // creating new TestSet
                        {
                            testSetName = line.Substring(line.IndexOf(":") + 2), //extracting test set name
                            passTotal = 0,
                            failTotal = 0,
                            setTotal = 0,
                            tCaseDictionary = new Dictionary<string, ResultEntity.TestCaseEntry>(), //creating dictionary to store subordinate test cases
                            testSetKey = (Guid.NewGuid()).ToString() //generating test set key
                        };
                        setCounter++;
                        caseCounter = 0;
                        string tsMessage = null;
                        DateTime setTime = new DateTime(); //setting inital set time
                        do
                        {
                            string tcMessage = null;
                            caseKey = "testCase" + caseCounter.ToString(); //creating key in parent TestSet dictionary to store TestCase in
                            if (Regex.Match(line, @"^FAIL: ", RegexOptions.Multiline).Success) //if failure messsage line is read
                            {
                                tcMessage += line;
                                tcName = Regex.Match(line, @"test_\d+_\w+", RegexOptions.Multiline).Value; //extract TestCase name
                                line = file.ReadLine();
                                do
                                {
                                    line = file.ReadLine();
                                    tcMessage += line;
                                }
                                while (!Regex.Match(line, @"^AssertionError: ", RegexOptions.Multiline).Success); //read till AssertionError message
                                tsMessage += tcMessage;
                                failedTC.Add(tcName, tcMessage); //add TestCase to dictionary of failed test cases
                                tcMessage = null;
                            }
                            if (Regex.Match(line, @"^(Ran )(\d+ )(tests in )((\d+).(\d+))(s)", RegexOptions.Multiline).Success) //if "Ran x tests in y s" message is read
                            {
                                newTS.tSetDictionary[setKey].setTotal += Int32.Parse(Regex.Match(line, @"\d+").Value); //read number of tests
                                string temp = Regex.Match(line, @"((\d+\.\d+))").Value;
                                newTS.tSetDictionary[setKey].runDuration = float.Parse(temp); //read duration of tests
                                newTS.runDuration += newTS.tSetDictionary[setKey].runDuration;
                            }
                            if (Regex.Match(line, @"failures=\d+", RegexOptions.Multiline).Success) //if "failures=" is read
                            {
                                newTS.tSetDictionary[setKey].failTotal = Int32.Parse(Regex.Match(line, @"\d+", RegexOptions.Multiline).Value); //parse number of failure
                            }
                            if (Regex.Match(line, @"^START:", RegexOptions.Multiline).Success) //if "START:" trigger is read
                            {
                                tcMessage = line;
                                tcName = Regex.Match(line, @"test_\d+_\w+", RegexOptions.Multiline).Value; //parse testCase name
                                do
                                {
                                    line = file.ReadLine();
                                    if (line != null)
                                    {
                                        tcMessage += line;
                                    }
                                }
                                while (!Regex.Match(line, @"^END:", RegexOptions.Multiline).Success); //read all line until "END:" trigger is read
                                tsMessage += tcMessage;
                                if (failedTC.ContainsKey(tcName)) //if the test case is in the failed test case dictionary
                                {
                                    newTS.tSetDictionary[setKey].tCaseDictionary[caseKey] = new ResultEntity.TestCaseEntry { //create a new test case that has the fail message concatenated onto the newly read message
                                        testCaseName = tcName, 
                                        message = failedTC[tcName] + tcMessage,
                                        result = "F",
                                    };
                                }
                                else
                                {
                                    newTS.tSetDictionary[setKey].tCaseDictionary[caseKey] = new ResultEntity.TestCaseEntry { //create a new test case normally
                                        testCaseName = tcName, 
                                        message = tcMessage,
                                        result = "P",
                                    };
                                }
                                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].testSetKey = newTS.tSetDictionary[setKey].testSetKey; //set new test case's set key to parent key
                                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].testCaseKey = (Guid.NewGuid()).ToString(); //generate new test case key
                            }
                            if (Regex.Match(line, @"^Runtime: ", RegexOptions.Multiline).Success) //if "Runtime:" trigger is read
                            {
                                //read the runtime of the testCase and parse it into the testCase object
                                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime = Convert.ToDateTime(line.Substring(line.IndexOf(":") + 2));
                                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].rtYear = (newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime).Year;
                                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].rtMonth = (newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime).Month;
                                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].rtDay = (newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime).Day;
                                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].rtHour = (newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime).Hour;
                                newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].rtMinute = (newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime).Minute;
                                setTime = newTS.tSetDictionary[setKey].tCaseDictionary[caseKey].runTime; //set set runtime to current testCases's run time, final set runtime will be the runtime of the last testCase
                                caseCounter++;
                            }
                            if (Regex.Match(line, @"echo", RegexOptions.Multiline).Success) //if "echo" is read
                            {
                                break; //the testSet is over, break the loop
                            }
                        }
                        while ((line = file.ReadLine()) != null); //when loop is exited, all test cases have been read in this set
                        newTS.tSetDictionary[setKey].testSuiteKey = newTS.testSuiteKey;
                        newTS.tSetDictionary[setKey].passTotal = newTS.tSetDictionary[setKey].setTotal - newTS.tSetDictionary[setKey].failTotal;
                        if (newTS.tSetDictionary[setKey].passTotal == newTS.tSetDictionary[setKey].setTotal)
                        {
                            newTS.passedTestSets++; //if the passTotal and setTotal of the set are equal, no test cases were failed, and the testSet counts as passed for the testSuite. if >=1 test cases were failed, the whole testSet counts as a fail
                        }
                        newTS.tSetDictionary[setKey].setMessages = tsMessage;
                        newTS.tSetDictionary[setKey].runTime = setTime;
                        newTS.tSetDictionary[setKey].rtYear = (newTS.tSetDictionary[setKey].runTime).Year;
                        newTS.tSetDictionary[setKey].rtMonth = (newTS.tSetDictionary[setKey].runTime).Month;
                        newTS.tSetDictionary[setKey].rtDay = (newTS.tSetDictionary[setKey].runTime).Day;
                        newTS.tSetDictionary[setKey].rtHour = (newTS.tSetDictionary[setKey].runTime).Hour;
                        newTS.tSetDictionary[setKey].rtMinute = (newTS.tSetDictionary[setKey].runTime).Minute;
                        suiteTime = newTS.tSetDictionary[setKey].runTime; //set suite runtime to current testSet's run time, final suite runtime will be the runtime of the last testSet
                    }
                }
                newTS.runTime = suiteTime;
                newTS.rtYear = (newTS.runTime).Year;
                newTS.rtMonth = (newTS.runTime).Month;
                newTS.rtDay = (newTS.runTime).Day;
                newTS.rtHour = (newTS.runTime).Hour;
                newTS.rtMinute = (newTS.runTime).Minute;
                DRFactory.DRFactory repoConnect = new DRFactory.DRFactory(); //create DRFactory object to facilitate use of DataRepo and its interface
                IDataRepo.IDataRepo IDRConnect = repoConnect.DRConnection(); //Use DRFactory to instantiate IDataRepo interface to allow for CRUD operations on database
                IDRConnect.CreateTestDB(); //create a database to store test results if one does not already exist
                IDRConnect.EnterTestSuite(newTS.testSuiteKey, btNum, newTS.passedTestSets, newTS.runDuration, newTS.runTime, newTS.rtYear, newTS.rtMonth, newTS.rtDay, newTS.rtHour, newTS.rtMinute); //enter in testSuite info
                foreach (ResultEntity.TestSetEntry testSet in newTS.tSetDictionary.Values) //for every testSet in the testSuite dictionary
                {
                    IDRConnect.EnterTestSet(testSet.testSuiteKey, testSet.testSetKey, testSet.testSetName, testSet.passTotal, testSet.failTotal, testSet.setTotal, testSet.setMessages, testSet.runDuration, testSet.runTime, testSet.rtYear, testSet.rtMonth, testSet.rtDay, testSet.rtHour, testSet.rtMinute); //enter testSet into
                    foreach (ResultEntity.TestCaseEntry testCase in testSet.tCaseDictionary.Values) //for every testCase in the given testSet's dictionary
                    {
                        IDRConnect.EnterTestCase(testCase.testSetKey, testCase.testCaseKey, testCase.testCaseName, testCase.result, testCase.message, testCase.runTime, testCase.rtYear, testCase.rtMonth, testCase.rtDay, testCase.rtHour, testCase.rtMinute); //enter testCase info
                    }
                }
            }
        }
    }
}

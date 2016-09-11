using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AppMain
{
    class AppMain
    {
        static void Main(string[] args)
        {
            List<string> btList = new List<string>(); //create list to store BT names
            //btList.Add(bt2);
            btList.Add("euvbenchtop3"); //adding bts to list
			btList.Add("euvbenchtop4");
			btList.Add("euvbenchtop5");
			btList.Add("euvbenchtop6");
			btList.Add("euvbenchtop7");
			btList.Add("euvbenchtop8");
            //btList.Add(bt9);
            //btList.Add(bt10);
            string target = Path.Combine(Directory.GetCurrentDirectory(), "inputFiles"); //path of directory in which pulled files from the BTs are stored
            int btNum = 3;
            foreach (string testBench in btList)
            {
				SessionManager mgr = new SessionManager(testBench);
                mgr.PullFromTarget(target, btNum); //calling SessionManager to pull all new html result files from target BT
                btNum++;
            }
            foreach (string fileName in Directory.GetFiles(target))
            {
					DataCollector.DataCollector input = new DataCollector.DataCollector(fileName, target);
					input.parseAndInput(); //calling DataCollector to parse target file
            }
            Directory.Delete(target, true); //Clean up directory used to store pulled BT files
        }
    }
}

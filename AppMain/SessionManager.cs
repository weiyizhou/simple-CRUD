using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WinSCP; //https://winscp.net/eng/docs/library provides through documentaion of this library

namespace AppMain
{
	class SessionManager
	{
		SessionOptions SessionConfig { get; set; }

		public SessionManager(string hostName) //constructor
		{
			SessionConfig = new SessionOptions() //initalize SessionOptions with the correct HostName
			{
				Protocol = WinSCP.Protocol.Sftp,
				HostName = hostName,
				UserName = "msc",
				Password = "msc",
				PortNumber = 60022,
				GiveUpSecurityAndAcceptAnySshHostKey = true
			};
		}

		public void PullFromTarget(string target, int btNumber) //Pull html result files from target BT
		{
			using (Session session = new Session()) //initalize temporary session
			{
				session.Open(this.SessionConfig); //open new temp session
				TransferOptions btOptions = new TransferOptions //create TransferOptions (allow for overriding)
				{
					OverwriteMode = WinSCP.OverwriteMode.Overwrite,
					TransferMode = TransferMode.Binary
				};
				RemoteDirectoryInfo dirInfo = session.ListDirectory(Properties.Settings.Default.targetDir); //get list of files in target directory (set in ini config)
				IOrderedEnumerable<RemoteFileInfo> remoteFiles = dirInfo.Files.Where(file => file.IsDirectory).OrderByDescending(file => file.Name); //put list in enum ordered by file name
				TransferOperationResult result = null;
				int suiteCounter = 0; //
				DRFactory.DRFactory factory = new DRFactory.DRFactory();
				IDataRepo.IDataRepo drConnect = factory.DRConnection();
                drConnect.CreatePFDB();

				if (!Directory.Exists(target)) //if the destination directory does not exist, create it
				{
					Directory.CreateDirectory(target);
				}

				foreach (RemoteFileInfo file in remoteFiles) //for each file in the target remote directory
				{
					if (Regex.Match(file.Name, @"^Results_", RegexOptions.Multiline).Success) //if the file name begins with "Results_", i.e. is an html result file
					{

						if (String.IsNullOrEmpty(drConnect.PullFile(file.Name))) //if the result of pulling the file name from the pulledFiles record data base is null, the file has not been pulled yet and should be pulled
						{
							result = session.GetFiles(session.EscapeFileMask(Properties.Settings.Default.targetDir + file.Name + "/stdout_stderr.html"), (Path.Combine(target, ("bt" + btNumber.ToString() + "_" + suiteCounter.ToString() + "stdout_stderr.*"))), false, btOptions); //pulling the html result file and saving it as bt_$(BT Number)_$(Suite Number)stdout_stderr.html
							drConnect.InsertPF(file.Name, file.LastWriteTime, file.LastWriteTime.Year, file.LastWriteTime.Month, file.LastWriteTime.Day, file.LastWriteTime.Hour, file.LastWriteTime.Minute); //inserting file into pulledFile database to mark it as having been pulled
							suiteCounter++; //increment suite counter

						}
					}
				}
			}
		}
	}
}

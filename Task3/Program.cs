namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TimeSpan deltaTime;
            int allowedDeltaMins = 1;
            List<string> ErrorLog = new List<string>(10);
            List<string> DeletionLog = new List<string>(10);
            DirectoryInfo directory;
            string folderPath;
            long initialFolderSize, currentFolderSize;
            Console.WriteLine("Enter the path");
            do
            {
                folderPath = Console.ReadLine();
                //folderPath = "D:\\Testing";
                directory = new DirectoryInfo(folderPath);
                if (!directory.Exists)
                {
                    Console.WriteLine("Directory does not exist. Try again");
                }
                else break;
            } while (true);

            DirectoryInfo[] dirs = directory.GetDirectories();

            initialFolderSize = getDirectorySize(directory);

            // Check directories
            try
            {
                foreach (DirectoryInfo dir in dirs)
                {
                    deltaTime = System.DateTime.Now - dir.LastAccessTime;
                    if (deltaTime.TotalMinutes > allowedDeltaMins)
                    {
                        try
                        {
                            // totSizeByte = getDirectorySize(dir);
                            dir.Delete(true); //folder to delete with all files
                            DeletionLog.Add(dir.Name);

                        }
                        catch (Exception e)
                        {
                            ErrorLog.Add(e.Message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.Append(e.Message);
            }

            // Check files
            try
            {
                FileInfo[] files = directory.GetFiles();
                foreach (FileInfo file in files)
                {
                    deltaTime = System.DateTime.Now - file.LastAccessTime;
                    if (deltaTime.TotalMinutes > allowedDeltaMins)
                    {
                        try
                        {
                            file.Delete();
                            DeletionLog.Add(file.Name);
                        }
                        catch (Exception e)
                        {
                            ErrorLog.Add(e.Message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.Append(e.Message);
            }


            currentFolderSize = getDirectorySize(directory);

            WriteStatus(initialFolderSize, currentFolderSize);

            if (ErrorLog.Count > 0)
            {
                WriteLog("Error log:", ErrorLog);
            }

            if (DeletionLog.Count > 0)
            {
                WriteLog("Deletion log:", DeletionLog);
            }
        }
        static long getDirectorySize(DirectoryInfo dir)
        {
            long len = 0;
            FileInfo[] files;


            DirectoryInfo[] dirs = dir.GetDirectories();

            if (dirs.Length == 0)
            {
                files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    len += file.Length;
                }

                return len;
            }
            files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                len += file.Length;
            }
            foreach (var item in dirs)
            {
                len += getDirectorySize(item);
            }
            return len;
        }
        static void WriteLog(string type, List<string> log)
        {
            Console.WriteLine(type);
            foreach (var item in log)
            {
                Console.WriteLine(item);
            }

        }
        static void WriteStatus(long size1, long size2)
        {
            Console.WriteLine("Initial folder size: {0}", size1);
            Console.WriteLine("Released space: {0}", size1 - size2);
            Console.WriteLine("Current folder size: {0}", size2);
            Console.WriteLine();
        }
    }
}

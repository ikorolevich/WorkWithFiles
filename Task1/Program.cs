namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TimeSpan deltaTime;
            int allowedDeltaMins = 30;
            List<string> ErrorLog = new List<string>(10);
            List<string> DeletionLog = new List<string>(10);
            DirectoryInfo directory;
            //string folderPath = Console.ReadLine();
            //string folderPath = "D:\\ZSLT - Cop";
            string folderPath;
            Console.WriteLine("Enter the path");
            do
            {
                //folderPath = Console.ReadLine();
                folderPath = "D:\\ZSLT - Copy";
                directory = new DirectoryInfo(folderPath);
                if (!directory.Exists)
                {
                    Console.WriteLine("Directory does not exist. Try again");
                }
                else break;
            } while (true);

            DirectoryInfo[] dirs = directory.GetDirectories();

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
                            DeletionLog.Add(dir.Name);
                            dir.Delete(true); //folder to delete with all files
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
                            DeletionLog.Add(file.Name);
                            file.Delete();
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


            if (ErrorLog.Count > 0)
            {
                Console.WriteLine("Eror log:");
                foreach (var item in ErrorLog)
                {
                    Console.WriteLine(item);
                }
            }

            if (DeletionLog.Count > 0)
            {
                Console.WriteLine("Deletion log:");
                foreach (var item in DeletionLog)
                {
                    Console.WriteLine(item);
                }
            }

            Environment.Exit(0);

        }
    }
}

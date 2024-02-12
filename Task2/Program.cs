using System.IO;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo directory;
            List<string> ErrorLog = new List<string>(10);
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
            double size = 0;
            foreach (var dir in dirs)
            {
                size += getDirs(dir);
            }

            try
            {
                foreach (DirectoryInfo dir in dirs)
                {
                   
                }
            }
            catch (Exception e)
            {
                ErrorLog.Append(e.Message);
            }
        }

        static double getDirs(DirectoryInfo dir)
        {
            double len = 0;

            FileInfo[] files;
            DirectoryInfo[] dirs = dir.GetDirectories();
            if (dirs.Length == 0)
            {
                files = dir.GetFiles();
                
                return files.Length;
            }
            foreach (var item in dirs)
            {
                files = item.GetFiles();
                len += files.Length;
                //foreach (var file in files)
                //{

                //}
                len = len + getDirs(item);
            }
            return len;
        }
    }
}

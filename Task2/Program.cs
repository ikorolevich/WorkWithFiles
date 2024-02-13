namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo directory;
            List<string> ErrorLog = new List<string>(10);
            string folderPath;

            Console.WriteLine("Enter the path");
            do
            {
                folderPath = Console.ReadLine();
                directory = new DirectoryInfo(folderPath);
                if (!directory.Exists)
                {
                    Console.WriteLine("Directory does not exist. Try again");
                }
                else break;
            } while (true);

            long totSizeByte = getDirectorySize(directory);
            double totSizeMB = totSizeByte / 1000000;
            double totSizeGB = totSizeByte / 1000000000;
            Console.WriteLine("Total size in bytes {0}, MB = {1}, Gb = {2}", totSizeByte, totSizeMB, totSizeGB);
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
    }
}

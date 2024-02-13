namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            FileInfo studentFile = new FileInfo("D:\\students.dat");

            if (studentFile.Exists)
            {
                FileStream studentStream = studentFile.OpenRead();
                using (BinaryReader br = new BinaryReader(studentStream))
                {
                    while (studentStream.Position < studentStream.Length)
                    {
                        Student student = new Student();
                        student.Name = br.ReadString();
                        student.Group = br.ReadString();
                        student.DateOfBirth = DateTime.FromBinary(br.ReadInt64());
                        student.AverageScore = br.ReadDecimal();
                        students.Add(student);
                    }

                }

            }
            else Console.WriteLine("File does not exist");

            DirectoryInfo workDirectory = new DirectoryInfo("D:\\Students");

            if (!workDirectory.Exists)
            {
                workDirectory.Create();
            }

            foreach (Student student in students)
            {
                string fileGroupPath = workDirectory.FullName + "\\" + student.Group + ".txt";
                FileInfo NewFile = new FileInfo(fileGroupPath);
                if (!NewFile.Exists)
                {
                    NewFile.Create().Close(); 
                }
                
                using (StreamWriter sw = NewFile.AppendText())
                {
                    sw.Write(student.Name);
                    sw.Write('\t');
                    sw.Write(student.DateOfBirth);
                    sw.Write('\t');
                    sw.Write(student.AverageScore);
                    sw.Write('\n');
                }

            }
        }
    }
}

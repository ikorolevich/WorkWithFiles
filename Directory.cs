using System;


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

public class Class1
{
	public Class1()
	{
	}
}

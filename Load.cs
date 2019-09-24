using System.Collections.Generic;

namespace FileReader
{
    public class Load
    {
        public List<IDictionary<string, string>> LoadFile(string localFile, string separator = ";")
        {
            List<IDictionary<string, string>> valueTreated = new List<IDictionary<string, string>>();
            if (localFile.Contains(".xlsx") || localFile.Contains(".xls"))
            {
                ReadExcel readExcel = new ReadExcel();
                valueTreated = readExcel.ReadFileExcel(localFile);
            }
            else if (localFile.Contains(".csv") || localFile.Contains(".txt"))
            {
                ReadFile readFile = new ReadFile();
                valueTreated = readFile.ReadFileCSV(localFile, separator);
            }
            return valueTreated;
        }
    }
}
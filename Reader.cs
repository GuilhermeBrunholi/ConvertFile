using System.IO.Compression;
using System.Collections.Generic;
using FileReader.model;

namespace FileReader
{
    public class FileReader
    {
        public ModelReturn LoadFile(string localFile, string separator = ";")
        {
            ModelReturn valueTreated = new ModelReturn();
            if (localFile.Contains(".xlsx") || localFile.Contains(".xls"))
            {
                ReadExcel readExcel = new ReadExcel();
                valueTreated.fileReturnList = readExcel.ReadFileExcel(localFile);
            }
            else if (localFile.Contains(".csv") || localFile.Contains(".txt"))
            {
                ReadFile readFile = new ReadFile();
                valueTreated.fileReturnList = readFile.ReadFileCSV(localFile, separator);
            }
            else
            {
                valueTreated.error = "ERROR: File type or Separator not accepted!";
            }
            return valueTreated;
        }

        public ModelReturn LoadZip(string localPath, string localExtract)
        {
            ModelReturn listPath = new ModelReturn();
            if (!string.IsNullOrEmpty(localPath) && (!string.IsNullOrEmpty(localExtract)) /*&& localPath.Contains(".zip")*/)
            {
                ReadZip readZip = new ReadZip();
                listPath = readZip.ReadPathZip(localPath, localExtract);
                return listPath;
            }
            else
            {
                listPath.error = "ERROR: File type not accepted!";
                return listPath;
            }
        }
    }
}
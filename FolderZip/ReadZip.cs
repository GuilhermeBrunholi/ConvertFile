using System;
using System.Text;
using System.Linq;
using FileReader.model;
using System.IO.Compression;
using System.Collections.Generic;

namespace FileReader
{
    class ReadZip
    {
        // Validar tipo de arquivo
        public ModelReturn ReadPathZip(string localFile)
        {
            string path;
            ModelReturn modelReturn = new ModelReturn();
            using (ZipArchive archive = ZipFile.Open(localFile, ZipArchiveMode.Update))
            {
                path = localFile + @"\" + TreatFolder(archive);
                if (path == null)
                {
                    modelReturn.error = "file type is not allowed";
                    return modelReturn;
                }
            }

            if (!string.IsNullOrEmpty(localFile))
            {
                if (path.Contains(".csv") || path.Contains(".txt"))
                {
                    ReadFile readFile = new ReadFile();
                    modelReturn.fileReturnList = readFile.ReadFileCSV(path, ";");
                }
                else if (path.Contains(".xls") || path.Contains(".xlsx"))
                {
                    ReadExcel readExcel = new ReadExcel();
                    modelReturn.fileReturnList = readExcel.ReadFileExcel(path);
                }
            }
            else
            {
                modelReturn.error = "No valid files found";
            }

            return modelReturn;
        }

        private static string TreatFolder(ZipArchive folder)
        {
            string listPath = "";
            for (int i = 0; i < folder.Entries.Count; i++)
            {
                string pathFile = folder.Entries[i].FullName.ToString();
                if (pathFile.Contains("."))
                {
                    if (pathFile.Contains(".txt") || pathFile.Contains(".csv") || pathFile.Contains(".xls") || pathFile.Contains(".xlsx"))
                    {
                        listPath = pathFile.Replace("/", @"\");
                        break;
                    }
                    else
                    {
                        listPath = null;
                        break;
                    }
                }
            }
            return listPath;
        }
    }
}

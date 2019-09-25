using System;
using System.IO;
using System.Data;
using System.Text;
using System.Linq;
using ExcelDataReader;
using System.Collections.Generic;

namespace FileReader
{
    class ReadExcel
    {
        // Ler arquivos .xls e .xlsx
        public List<IDictionary<string, string>> ReadFileExcel(string localFile)
        {
            DataSet arq;
            using (var stream = File.Open(localFile, FileMode.Open, FileAccess.Read))
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    arq = reader.AsDataSet();
                    reader.Close();
                }
            }

            var itemsFile = ReadTable(arq);
            TreatFile(itemsFile);
            NameColumns(itemsFile);
            var treatedList = CreatNameList(itemsFile);
            return treatedList;
        }

        public List<List<string>> ReadTable(DataSet arq)
        {
            List<List<string>> listIndexs = new List<List<string>>();
            List<List<string>> listItems = new List<List<string>>();
            for (int t = 0; t < arq.Tables.Count; t++)
            {
                for (int r = 0; r < arq.Tables[t].Rows.Count; r++)
                {
                    List<string> items = new List<string>();
                    for (int c = 0; c < arq.Tables[t].Columns.Count; c++)
                    {
                        items.Add(Convert.ToString(arq.Tables[t].Rows[r][c]));
                    }
                    if (r == 0)
                        listIndexs.Add(items);
                    else
                        listItems.Add(items);
                }
            }

            List<string> maxSize = new List<string>();
            for (int i = 0; i < listIndexs.Count; i++)
            {
                if (maxSize.Count < listIndexs[i].Count)
                {
                    maxSize = listIndexs[i];
                }
            }

            List<List<string>> finalList = new List<List<string>>();
            finalList.Add(maxSize);
            for (int i = 0; i < listItems.Count; i++)
            {
                finalList.Add(listItems[i]);
            }
            return finalList;
        }

        public void TreatFile(List<List<string>> list)
        {
            for (int a = 1; a < list.Count; a++)
            {
                if (list[0].Count > list[a].Count)
                {
                    int count = list[a].Count;
                    while (list[0].Count > list[a].Count)
                    {
                        list[a].Add("");
                        count++;
                    }

                }
            }
        }

        public void NameColumns(List<List<string>> list)
        {
            var items = list[0];
            for (int z = 0; z < items.Count; z++)
            {
                if (items[z] == "" || items[z] == null)
                {
                    items[z] = "Column" + z;
                }
            }
        }

        public List<IDictionary<string, string>> CreatNameList(List<List<string>> list)
        {
            var indexs = list[0];
            List<List<string>> items = new List<List<string>>();
            for (int i = 1; i < list.Count; i++)
            {
                items.Add(list[i]);
            }
            var treated = NameIndexes(indexs, items);
            return treated;
        }

        public List<IDictionary<string, string>> NameIndexes(List<string> indexes, List<List<string>> listValues)
        {
            List<IDictionary<string, string>> listFinal = new List<IDictionary<string, string>>();
            for (int y = 0; y < listValues.Count; y++)
            {
                var listInside = listValues[y];
                IDictionary<string, string> list = new Dictionary<string, string>();
                for (int z = 0; z < listInside.Count; z++)
                {
                    list.Add(indexes[z], listInside[z]);
                }
                listFinal.Add(list);
            }
            return listFinal;
        }
    }
}

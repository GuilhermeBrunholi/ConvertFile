using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace FileReader
{
    class ReadFile
    {
        // ler arquivos .csv e .txt
        public List<IDictionary<string, string>> ReadFileCSV(string linkFile, string separator)
        {
            List<string> rows = new List<string>();
            using (StreamReader sr = new StreamReader(linkFile))
            {
                string contentFile = sr.ReadToEnd();
                var contentList = contentFile.Split("\r\n").ToList();
                var lastIndex = contentList.Count - 1;

                if (string.IsNullOrEmpty(contentList[lastIndex]))
                {
                    contentList.RemoveAt(lastIndex);
                }

                for (int i = 0; i < contentList.Count; i++)
                {
                    var list = contentList[i];
                    rows.Add(list);
                }
            }

            string nameColumn = rows[0];
            rows.RemoveAt(0);
            var columns = TreatIndexList(nameColumn.Split(separator).ToList());
            List<IDictionary<string, string>> content = new List<IDictionary<string, string>>();
            for (int i = 0; i < rows.Count; i++)
            {
                IDictionary<string, string> tempList = new Dictionary<string, string>();
                var row = rows[i].Split(separator);
                for (int y = 0; y < row.Length; y++)
                {
                    tempList.Add(columns[y], row[y]);
                }
                content.Add(tempList);
            }
            return content;
        }

        private static List<string> TreatIndexList(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == "" || list[i] == " " || list[i] == null)
                {
                    list[i] = "Column" + i;
                }
            }
            return list;
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;

namespace FileReader.model
{
    public class ModelReturn
    {
        public List<IDictionary<string, string>> fileReturnList { get; set; }
        public string error { get; set; }
    }
}

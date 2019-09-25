using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FileReader.ConvertToJson
{
    public class ToJson
    {
        public void ConvertList(List<string> indices, List<List<string>> valoresLista)
        {
            List<IDictionary<string, string>> listaFinal = new List<IDictionary<string, string>>();
            for (int y = 0; y < valoresLista.Count; y++)
            {
                var listaDentro = valoresLista[y];
                IDictionary<string, string> lista = new Dictionary<string, string>();
                for (int z = 0; z < listaDentro.Count; z++)
                {
                    //Console.WriteLine(indices[z] + ": " + listaDentro[z]);
                    lista.Add(indices[z], listaDentro[z]);
                }
                listaFinal.Add(lista);
            }
            Console.WriteLine(JsonConvert.SerializeObject(listaFinal));
        }
    }
}
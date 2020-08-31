using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmaps.Model
{
    class DataManager
    {

        private const string PATH = "D:\\Documentos\\VisualStudioWorkSpace\\GmapsBeta\\Gmaps\\Data\\data.csv";
        List<string> lista;

        public DataManager()
        { 
            
            lista = new List<string>();
            readInfo();

        }

        private void readInfo()
        {

            var reader = new StreamReader(File.OpenRead(PATH));
            int count = 0;
            while (!reader.EndOfStream && count < 100)
            {

                var line = reader.ReadLine();
                var arreglo = line.Split(',');

                lista.Add(arreglo[4] + ", Colombia");
                count++;
            }

        }

        public List<string> getLista()
        {

            return lista;
        
        }

    }
}

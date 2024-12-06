using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Kalapacsvetes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "kalapacsvetes.txt"; 
            List<Sportolo> sportolok = new List<Sportolo>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++) 
                {
                    string[] data = lines[i].Split(';');

                    int helyezes = int.Parse(data[0]);
                    double eredmeny = double.Parse(data[1].Replace(',', '.'), CultureInfo.InvariantCulture);
                    string nev = data[2];
                    string orszagkod = data[3];
                    string helyszin = data[4];
                    DateTime datum = DateTime.Parse(data[5]);

                    Sportolo sportolo = new Sportolo(helyezes, eredmeny, nev, orszagkod, helyszin, datum);
                    sportolok.Add(sportolo);
                }

                Console.WriteLine("Sportolók adatai:");
                foreach (var sportolo in sportolok)
                {
                    Console.WriteLine(sportolo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }
    }
}


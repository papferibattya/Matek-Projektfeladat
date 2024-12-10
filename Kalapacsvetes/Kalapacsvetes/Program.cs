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
            //Itt beolvassuk majd kiiratjuk a fájl tartalmát.
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

            Console.WriteLine("\nStatisztika az adatokról");
            //Itt számoljuk ki az átlagot szórást minimum és maximumot.
            double atlag = sportolok.Average(Sportolo => Sportolo.Eredmeny);
            Console.Write($"\nEredmények átlaga: {atlag} m");
            
            double szoras = Math.Sqrt(sportolok.Average(Sportolo => Math.Pow(Sportolo.Eredmeny - atlag, 2)));
            Console.Write($"\nEredmények szórása: {szoras:F2} m");
            
            double minimum = sportolok.Min(Sportolo => Sportolo.Eredmeny);
            Console.Write($"\nEredmények minimuma: {minimum} m");
            
            double maximum = sportolok.Max(Sportolo => Sportolo.Eredmeny);
            Console.Write($"\nEredmények maximuma: {maximum} m");


           // Itt helyszín és minimális eredmény alapján szűrünk amit majd kiiratunk konzolra és txt fájlba is.
            Console.Write("\nAdd meg a helyszínt, amelyre szűrni szeretnél: ");
            string szurtHelyszin = Console.ReadLine();

            Console.Write("\nAdd meg a minimális eredményt: ");
            double szurtEredmeny = double.Parse(Console.ReadLine());

            
            List<string> szurtSportolok = new List<string>();
            foreach (var sportolo in sportolok)
            {
                if (sportolo.Helyszin == szurtHelyszin && sportolo.Eredmeny >= szurtEredmeny)
                {
                    szurtSportolok.Add($"{sportolo.Nev} - {sportolo.Eredmeny}m");
                }
            }

            
            Console.Write("\nSzűrés eredménye a konzolon:");
            foreach (var sor in szurtSportolok)
            {
                Console.Write(sor);
            }

            
            string fajlNev = "szurt_eredmenyek.txt";
            File.WriteAllLines(fajlNev, szurtSportolok);

            Console.Write($"\nAz eredményeket elmentettem a(z) {fajlNev} fájlba.");

            Console.ReadLine();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace opening_weekend
{
    class Film
    {
        public string eredeticim { get; set; }
        public string magyarcim { get; set; }
        public string bemutato { get; set; }
        public string forgalmazo { get; set; }
        public int bevel { get; set; }
        public int latogato { get; set; }

        public Film(string sor)
        {
            string[] t = sor.Split(';');
            eredeticim = t[0];
            magyarcim = t[1];
            bemutato = t[2];
            forgalmazo = t[3];
            bevel = int.Parse(t[4]);
            latogato = int.Parse(t[5]);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Film> Filmek = new List<Film>();

            string[] adatok = File.ReadAllLines("nyitohetvege.txt");
            foreach (var sor in adatok.Skip(1))     
            {
                Film F = new Film(sor);
                Filmek.Add(F);
            }

            Console.WriteLine($"3.feladat: Filmek száma az állományban: {Filmek.Count} db");

            int Osszbevetel = 0;
            foreach (var f in Filmek)
            {
                if (f.forgalmazo=="UIP")
                {
                    //Osszbevetel = Osszbevetel + f.bevel;
                    Osszbevetel += f.bevel;
                }
            }
            Console.WriteLine($"4.feladat: UIP Duna Film forgalmazó 1. hetes bevételeinek összege: {Osszbevetel} Ft");
            Film legtobbLatogato = Filmek.OrderBy(f => f.latogato).Last();

            Console.WriteLine("5. feladat: Legtöbb látogató az első héten:");
            Console.WriteLine($"\tEredeti cím: {legtobbLatogato.eredeticim}");
            Console.WriteLine($"\tMagyar cím: {legtobbLatogato.magyarcim}");
            Console.WriteLine($"\tForgalmazó: {legtobbLatogato.forgalmazo}");
            Console.WriteLine($"\tBevétel az első héten: {legtobbLatogato.bevel.ToString("C2")}");
            Console.WriteLine($"\tLátogatók száma: {legtobbLatogato.latogato} fő");

            bool vanFilm = false;
            Filmek.ForEach((f) => {
                string[] dbMagyarCim = f.magyarcim.Split(' ');
                string[] dbEredetiCim = f.eredeticim.Split(' ');
                if (dbMagyarCim[0].StartsWith("W") && dbMagyarCim[1].StartsWith("W") && dbEredetiCim[0].StartsWith("W") && dbEredetiCim[1].StartsWith("W"))
                    vanFilm = true;
                Console.WriteLine(vanFilm ? "6. feladat: Volt ilyen film!" : "6. feladat: Nem volt ilyen film!");


            Dictionary<string, int> forgalmazoEsFilmek = new Dictionary<string, int>();

            Filmek.ForEach((f) => {
                if (!forgalmazoEsFilmek.ContainsKey(f.forgalmazo))
                {
                    forgalmazoEsFilmek.Add(f.forgalmazo, 1);
                }
                else
                {
                    forgalmazoEsFilmek[f.forgalmazo]++;
                }
            });

            List<string> fileKimenet = new List<string>();
            fileKimenet.Add("forgalmazo;filmekSzama");

            foreach (var f in forgalmazoEsFilmek)
            {
                if (f.Value > 1)
                {
                    fileKimenet.Add($"{f.Key};{f.Value}");
                }
            }

            File.WriteAllLines("stat.csv", fileKimenet);

            List<DateTime> interComBemutatok = new List<DateTime>();

            Filmek.ForEach((f) => {
                if (f.forgalmazo == "InterCom")
                {
                    interComBemutatok.Add(f.bemutato);
                }
            });

            int legnagyobbKulonbseg = 0;

            for (int i = 0; i < interComBemutatok.Count; i++)
            {
                if (i != 0)
                {
                    int kulonbsegSzamitott = (interComBemutatok[i] - interComBemutatok[i - 1]).Days;
                    if (kulonbsegSzamitott > legnagyobbKulonbseg)
                    {
                        legnagyobbKulonbseg = kulonbsegSzamitott;

                            Console.WriteLine($"8. feladat: A leghosszabb időszak két InterCom-os bemutató között: {legnagyobbKulonbseg} nap");


                            Console.ReadKey();
        }
    }
}

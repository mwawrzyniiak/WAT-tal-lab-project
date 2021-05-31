using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace HamiltonianCycleUI.Services
{
    public static class GraphGenerator
    {
        static int liczbaWierzcholkow = 5;
        static int liczbaKrawedzi = 10;
        static int wymaganaLiczbaKrawedzi = 2;
        static int maksymalnaLiczbaKrawedzi = 5;
        static List<string> WszystkieKrawedzie = new();
        static List<int> iloscKrawedziwWierzcholku = new();
        static int wagaMin = 5;
        static int wagaMax = 15;
        public static void GenerateGraphToFile()
        {
            Random rnd = new();
            wszystkieMozliweKrawedzie(WszystkieKrawedzie, liczbaWierzcholkow);

            for (int i = 0; i < liczbaWierzcholkow; i++)
            {
                iloscKrawedziwWierzcholku.Add(0);
            }

            var losoweKrawedzie = WszystkieKrawedzie.OrderBy(x => rnd.Next()).Take(liczbaKrawedzi).ToList();

            while (czyKrawedzieSpelniajaKryteria(losoweKrawedzie, iloscKrawedziwWierzcholku, wymaganaLiczbaKrawedzi, maksymalnaLiczbaKrawedzi))
            {
                losoweKrawedzie = WszystkieKrawedzie.OrderBy(x => rnd.Next()).Take(liczbaKrawedzi).ToList();
            }
            var wynikLosowania = losoweKrawedzie.OrderBy(x => x).ToList();

            for (int i = 0; i < wynikLosowania.Count; i++)
            {
                wynikLosowania[i] = wynikLosowania[i] + " " + rnd.Next(wagaMin, wagaMax);
            }

            GenerateDataToTxt(wynikLosowania);

        }

        private static void GenerateDataToTxt(List<string> wynik)
        {
            StreamWriter sw = new StreamWriter(@"..\..\..\ExampleData\graphGenerator.txt");
            sw.WriteLine(liczbaWierzcholkow + " " + liczbaKrawedzi);

            foreach (var item in wynik)
            {
                sw.WriteLine(item);
            }
            sw.Flush();
        }

        private static void wszystkieMozliweKrawedzie(List<string> list, int liczbaWierzcholkow)
        {
            for (int i = 0; i < liczbaWierzcholkow; i++)
            {
                for (int j = 1; j < liczbaWierzcholkow; j++)
                {
                    if (i < j)
                    {
                        list.Add(i.ToString() + " " + j.ToString());
                    }
                }
            }
        }

        public static bool czyKrawedzieSpelniajaKryteria(List<string> all, List<int> licznik, int wymaganaLiczbaKrawedzi, int maksymalnaLiczbaKrawedzi)
        {
            for (int i = 0; i < licznik.Count; i++)
            {
                licznik[i] = 0;
            }
            foreach (var item in all)
            {
                var item1temp = item.Substring(0, item.IndexOf(" "));
                int item1 = int.Parse(item1temp);
                var koniec = item.Length - (item.IndexOf(" ") + 1);
                var item2temp = item.Substring(item.IndexOf(" ") + 1, koniec);
                var item2 = int.Parse(item2temp);
                licznik[item1] += 1;
                licznik[item2] += 1;
            }
            foreach (var item in licznik)
            {
                if (item < wymaganaLiczbaKrawedzi || item > maksymalnaLiczbaKrawedzi)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

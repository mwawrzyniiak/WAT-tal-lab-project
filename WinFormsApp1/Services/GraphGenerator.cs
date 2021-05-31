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

        static List<string> WszystkieKrawedzie = new();
        static List<int> WszystkieWierzcholki = new();
        public static void GenerateGraphToFile()
        {
            Random rnd = new();
            wszystkieMozliweKrawedzie(WszystkieKrawedzie, liczbaWierzcholkow);

            for (int i = 0; i < liczbaWierzcholkow; i++)
            {
                WszystkieWierzcholki.Add(0);
            }


            var asdc = WszystkieKrawedzie.OrderBy(x => rnd.Next()).Take(liczbaKrawedzi).ToList();
            while (!Check(asdc, WszystkieWierzcholki))
            {
                asdc = WszystkieKrawedzie.OrderBy(x => rnd.Next()).Take(liczbaKrawedzi).ToList();
            }
            var wynik = asdc.OrderBy(x => x).ToList();

            for (int i = 0; i < wynik.Count; i++)
            {
                wynik[i] = wynik[i] + " " + rnd.Next(1, 20);
            }


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
                    if (i < j)
                    {
                        list.Add(i.ToString() + " " + j.ToString());
                    }
            }
        }

        public static bool Check(List<string> all, List<int> licznik)
        {
            foreach (var item in all)
            {
                var item1 = (int)item.First() - 48;
                var item2 = (int)item.Last() - 48;
                licznik[item1] += 1;
                licznik[item2] += 1;
            }
            if (licznik.Any(x => x >= 2))
            {
                return true;
            }
            return false;

        }
    }
}

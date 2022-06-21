using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Kruszkal
{

    public class Edge
    {
        public int weight;
        public int beg;
        public int end;
        public Edge(int beg, int end, int weight)
        {
            this.beg = beg;
            this.end = end;
            this.weight = weight;
        }
        public int getBeg()
        {
            return beg;
        }
        public int getEnd()
        {
            return end;
        }
        public int getWeight()
        {
            return weight;
        }
    }


    class Program
    {
        public static int NodeComparer(int[] Node, int x, int y)
        {
            if (Node[x] != Node[y])
            {
                for (int j = 1; j < Node.Count(); j++)
                {
                    if (Node[j] == x)
                    {
                        Node[j] = y;
                    }
                }
                return 1;
            }
            return 0;
        }
        static void Main(string[] args)
        {

            int j = 0;
            int v = default;
            int k = 0;
            int count = 0;
            List<Edge>[] tab = default;
            try
            {
                using (StreamReader sr = new StreamReader("In0303.txt"))
                {
                    string line;
                    string[] spliteline;
                    line = sr.ReadLine();
                    var n = Int32.Parse(line);
                    tab = new List<Edge>[n];

                    while ((line = sr.ReadLine()) != null)
                    {
                        spliteline = line.Split(' ');
                        var result = spliteline.Select(e => int.Parse(e)).ToList();
                        List<Edge> Edges = new List<Edge>();
                        for (int i = 0; i < result.Count; i++)
                        {
                            Edges.Add(new Edge(j + 1, result[i], result[i + 1]));
                            i++;
                            count++;
                        }
                        tab[j] = Edges;
                        j++;
                    }
                    v = n;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Edge[] EdgeTab = new Edge[count];//lista do posortowania
            foreach (List<Edge> edges in tab)
            {
                foreach (Edge edge in edges)
                {
                    EdgeTab[k] = edge;
                    k++;
                }
                edges.Clear();
            }
            Edge[] Sort(Edge[] tab, int beginning, int end)
            {
                int middle = tab[(beginning + end) / 2].getWeight();
                int i, j;
                Edge x = default;
                i = beginning;
                j = end;

                do
                {
                    while (tab[i].getWeight() < middle) i++;
                    while (tab[j].getWeight() > middle) j--;
                    if (i <= j)
                    {
                        x = tab[i];
                        tab[i] = tab[j];
                        tab[j] = x;
                        i++;
                        j--;
                    }
                } while (i <= j);
                if (j > beginning) Sort(tab, beginning, j);
                if (i < end) Sort(tab, i, end);
                return tab;
            }
            Sort(EdgeTab, 0, count - 1);
            foreach (Edge edge in EdgeTab.ToList())
            {
                foreach (Edge edge1 in EdgeTab.ToList())
                {
                    if (edge.beg == edge1.end && edge.end == edge1.beg)
                    {
                        if (edge.beg > edge1.beg)
                        {
                            edge.weight = 10;
                        }
                        else
                        {
                            edge1.weight = 10;
                        }
                    }

                }
                if (edge.weight != 10)
                {
                    tab[edge.weight - 1].Add(edge);
                }
            }
            foreach (List<Edge> edges in tab)
            {
                edges.Sort(delegate (Edge e1, Edge e2)
                {
                    return e1.beg.CompareTo(e2.beg);
                });
                foreach (Edge edge in edges)
                {
                    
                }
            }
            List<Edge> ResultMST = new List<Edge>();
            int[] Node = new int[v + 1];
            int valueofweight = 0;
            for (int i = 0; i < v + 1; i++)
            {
                Node[i] = i;
            }
            foreach (List<Edge> edges in tab)
            {
                foreach (Edge edge in edges)
                {
                    if (ResultMST.Count == 0)
                    {
                        ResultMST.Add(edge);
                        valueofweight += edge.weight;
                    }
                    else
                    {
                        if (NodeComparer(Node, edge.beg, edge.end) == 1)
                        {
                            ResultMST.Add(edge);
                            valueofweight += edge.weight;
                            if (ResultMST.Count == v - 1) goto FinishResult;
                        }
                    }
                }
            }
        FinishResult:
            using (StreamWriter sw = new StreamWriter("Out0303.txt"))
            {
                foreach (Edge edge in ResultMST)
                {
                    sw.Write(edge.beg+" ");
                    sw.Write(edge.end + " ");
                    sw.Write("["+edge.weight + "], ");
                    
                }
                sw.WriteLine();
                sw.Write(valueofweight);
            }
            string see = File.ReadAllText("Out0303.txt");
            Console.WriteLine(see);
        }
    }
}
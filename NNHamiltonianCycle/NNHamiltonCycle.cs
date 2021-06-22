using Graph.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNHamiltonianCycle
{
    public class NNHamiltonCycle
    {
        private List<bool> visited = new List<bool>();

        private GraphModel graph;
        private StringBuilder stringBuilder = new StringBuilder();

        private long vertexCount;

        private List<long> weights = new List<long>();
        private List<int> stackSymulation = new List<int>();

        private List<List<int>> hamiltonCycles = new List<List<int>>();

        public NNHamiltonCycle(GraphModel graph)
        {
            this.graph = graph;

            PrepareVariables();
            var startTime = DateTime.Now;
            foreach (var vertex in graph.Graph)
                NNHamiltonCycleAlgorithm((int) vertex.Id);
            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine("Czas wykonania algorytmu DFS(dokładność do 100ns) " + elapsedTime.Ticks);
        }

        public string GetHamiltonCycle()
        {
            return stringBuilder.ToString();
        }

        public List<List<int>> GetAllHamiltonCycles()
        {
            return hamiltonCycles;
        }

        private void NNHamiltonCycleAlgorithm(int startingVertex)
        {
            stackSymulation.Clear();
            ClearVisitedStruct();

            long weight = 0;
            visited[startingVertex] = true;
            stackSymulation.Add(startingVertex);

            while (!IsAllVisited())
            {
                int currentVertex = stackSymulation[stackSymulation.Count - 1];
                long weightTemp = long.MaxValue;

                for(int i = 0; i < graph.Graph[currentVertex].Weights.Count; i++)
                {
                    if (weightTemp >= graph.Graph[currentVertex].Weights[i] && !stackSymulation.Contains((int) graph.Graph[currentVertex].Neighbors[i].Id))
                    {
                        weightTemp = graph.Graph[currentVertex].Weights[i];
                    }
                }

                int indexTemp=0;
                foreach (var neighWeigh in graph.Graph[currentVertex].Weights)
                {
                    if (neighWeigh == weightTemp)
                    {
                        if (!stackSymulation.Contains((int) graph.Graph[currentVertex].Neighbors[indexTemp].Id))
                            break;
                    }

                    indexTemp++;
                }

                if (indexTemp >= graph.Graph[currentVertex].Weights.Count)
                    break;

                var tempVertex = graph.Graph[currentVertex].Neighbors[indexTemp];

                if(visited[(int)tempVertex.Id] == false)
                {
                    visited[(int)tempVertex.Id] = true;
                    stackSymulation.Add((int)tempVertex.Id);
                    weight += weightTemp;
                }
            }

            bool isExistHamiltonCycle = false;

            if(IsAllVisited())
            {
                foreach (var vertex in graph.Graph[stackSymulation[stackSymulation.Count - 1]].Neighbors)
                {
                    if (vertex.Id == startingVertex)
                    {
                        isExistHamiltonCycle = true;
                        break;
                    }
                }
            }
            

            if(isExistHamiltonCycle)
            {
                List<int> cycle = new List<int>();
                foreach (var ver in stackSymulation)
                {
                    stringBuilder.Append(ver + " -> ");
                    cycle.Add(ver);
                }

                stringBuilder.Append(startingVertex);
                stringBuilder.Append(" Suma wag: " + weight);

                cycle.Add(startingVertex);
                hamiltonCycles.Add(cycle);
                stringBuilder.AppendLine("");
                weights.Add(weight);
            }
            else
            {
                stringBuilder.Append("BRAK CYKLU HAMILTONA DLA WIERZCHOŁKA O NUMERZE: " + startingVertex);
                stringBuilder.AppendLine("");
            }
        }
        
        private void PrepareVariables()
        {
            vertexCount = graph.NumberOfVertex;
            PrepareVisitedStruct();
        }

        private bool IsAllVisited()
        {
            foreach (var ver in visited)
                if (ver == false)
                    return false;

            return true;
        }

        private void PrepareVisitedStruct()
        {
            for (int i = 0; i < vertexCount; i++)
                visited.Add(false);
        }

        private void ClearVisitedStruct()
        {
            for (int i = 0; i < vertexCount; i++)
                visited[i] = false;
        }
    }
}

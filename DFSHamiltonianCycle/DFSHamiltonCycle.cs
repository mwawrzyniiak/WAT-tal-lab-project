using Graph.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DFSHamiltonianCycle
{
    public class DFSHamiltonCycle
    {
        private List<bool> visited = new List<bool>();
        private List<int> stackSymulation= new List<int>();

        private GraphModel graph;

        private int startVertexId;

        private int sptr;
        private long edgesCount, vertexCount;
        private int counter = 1;

        private bool isExistHamiltonCycle;

        private StringBuilder output = new StringBuilder();
        private List<List<int>> hamiltonCycles = new List<List<int>>();

        public DFSHamiltonCycle(GraphModel graph, int startingVertex)
        {
            this.graph = graph;
            startVertexId = startingVertex;

            PrepareVariables();
            var startTime = DateTime.Now;
            DFSHamiltonCycleAlgorithm(startingVertex);            
            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine("Czas wykonania algorytmu DFS(dokładność do 100ns) " + elapsedTime.Ticks);
        }

        public string GetHamiltonCycle()
        {
            return output.ToString();
        }

        public List<List<int>> GetAllHamiltonCycles()
        {
            return hamiltonCycles;
        }

        private void DFSHamiltonCycleAlgorithm(int startingVertex)
        {
            if (stackSymulation.Count <= sptr)
            {
                ++counter;
                stackSymulation.Add(startingVertex);
            }
            else
            {
                ++counter;
                stackSymulation[sptr] = startingVertex; ++counter;
            }

            sptr++;

            if (sptr < vertexCount)
            {
                visited[startingVertex] = true;
                ++counter;
                foreach (var vertex in graph.Graph[startingVertex].Neighbors) 
                {
                    if (!visited[(int)vertex.Id])
                    {
                        DFSHamiltonCycleAlgorithm((int)vertex.Id);
                        ++counter;
                    }
                }
                ++counter;
                visited[startingVertex] = false;
            }
            else
            {
                isExistHamiltonCycle = false;
   
                foreach (var vertex in graph.Graph[startingVertex].Neighbors)
                {
                    ++counter;
                    if (vertex.Id == startVertexId)
                    {
                        isExistHamiltonCycle = true;
                        break;
                    }
                }

                if(isExistHamiltonCycle)
                {
                    ++counter;
                    output.Append("CYKL HAMILTONA: ");
                    
                    var cycle = new List<int>();

                    foreach (var vertex in stackSymulation)
                    {
                        output.Append(vertex + " -> ");
                        cycle.Add(vertex);
                    }

                    output.Append(startVertexId);
                    cycle.Add(startVertexId);

                    output.AppendLine("");
                    hamiltonCycles.Add(cycle);
                }
            }

            sptr--;
        }

        private void PrepareVariables()
        {
            edgesCount = graph.NumberOfEdge;
            vertexCount = graph.NumberOfVertex;

            for (int i = 0; i < vertexCount; i++)
                visited.Add(false);

            sptr = 0;
        }
    }
}

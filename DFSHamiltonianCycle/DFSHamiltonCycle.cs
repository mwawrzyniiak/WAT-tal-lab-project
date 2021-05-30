using Graph.Models;
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

        private bool isExistHamiltonCycle;

        private StringBuilder output = new StringBuilder();
        private List<List<int>> hamiltonCycles = new List<List<int>>();

        public DFSHamiltonCycle(GraphModel graph, int startingVertex)
        {
            this.graph = graph;
            startVertexId = startingVertex;

            PrepareVariables();
            DFSHamiltonCycleAlgorithm(startingVertex);
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
                stackSymulation.Add(startingVertex);
            else
                stackSymulation[sptr] = startingVertex;
            
            sptr++;

            if (sptr < vertexCount)
            {
                visited[startingVertex] = true;
                foreach(var vertex in graph.Graph[startingVertex].Neighbors) 
                {
                    if (!visited[(int)vertex.Id])
                        DFSHamiltonCycleAlgorithm((int)vertex.Id);
                }

                visited[startingVertex] = false;
            }
            else
            {
                isExistHamiltonCycle = false;

                foreach(var vertex in graph.Graph[startingVertex].Neighbors)
                {
                    if (vertex.Id == startVertexId)
                    {
                        isExistHamiltonCycle = true;
                        break;
                    }
                }

                if(isExistHamiltonCycle)
                {
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

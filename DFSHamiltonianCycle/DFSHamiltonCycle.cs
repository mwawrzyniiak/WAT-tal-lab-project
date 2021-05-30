using Graph.Models;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DFSHamiltonianCycle
{
    public class DFSHamiltonCycle
    {
        private List<bool> visited = new List<bool>();
        private Stack stack = new Stack();

        private GraphModel graph;

        private int startVertexId;

        private long sptr;
        private long edgesCount, vertexCount;

        private bool isExistHamiltonCycle;

        private StringBuilder output = new StringBuilder();

        public string GetHamiltonCycle()
        {
            return output.ToString();
        }

        public DFSHamiltonCycle(GraphModel graph, int startingVertex)
        {
            this.graph = graph;
            startVertexId = startingVertex;

            PrepareVariables();
            DFSHamiltonCycleAlgorithm(startingVertex);
        }

        private void DFSHamiltonCycleAlgorithm(int startingVertex)
        {
            stack.Push(startingVertex);
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

                    output.Append(startVertexId);

                    foreach (var vertex in stack)
                        output.Append(vertex);

                    output.AppendLine("");
                }

                sptr--;
            }

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

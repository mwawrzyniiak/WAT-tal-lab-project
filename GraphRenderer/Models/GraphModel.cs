using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Models
{
    /// <summary>
    /// Graph model class
    /// Graph should be connected
    /// <param name="GraphModel"> List of vertex that building the graph </param>
    /// </summary>
    public class GraphModel
    {
        private List<VertexModel> graph;

        public List<VertexModel> Graph { get => graph; set => graph = value; }
        public long NumberOfEdge { get; set; }

        public GraphModel(List<VertexModel> graph)
        {
            this.graph = graph;
        }

        public GraphModel(long numberOfVerticies)
        {
            graph = new List<VertexModel>();

            for (int i = 0; i < numberOfVerticies; i++)
                graph.Add(new VertexModel(i));
        }

        public GraphModel()
        {
            graph = new List<VertexModel>();
        }

        internal void UpdateNeighbors(long vertexStart, long vertexEnd)
        {
            if (!IsVertexExistInGraph(vertexStart) || !IsVertexExistInGraph(vertexEnd) || vertexStart == vertexEnd)
                throw new Exception("Can't create Graph. Unknown vertex number");

            var VertexStart = graph.Where(x => x.Id == vertexStart).FirstOrDefault();
            var VertexEnd = graph.Where(x => x.Id == vertexEnd).FirstOrDefault();

            var idStart = graph.IndexOf(VertexStart);
            var idEnd = graph.IndexOf(VertexEnd);

            graph[idStart].Neighbors.Add(VertexEnd);
            graph[idEnd].Neighbors.Add(VertexStart);
        }

        private bool IsVertexExistInGraph(long vertexId)
        {
            foreach (var vertex in graph)
            {
                if (vertex.Id == vertexId)
                    return true;
            }

            return false;
        }
    }
}

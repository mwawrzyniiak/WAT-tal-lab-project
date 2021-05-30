using Graph.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Graph.Loader
{
    public class GraphLoader
    {
        private static GraphLoader instance;

        private long numberOfVertices = 0;
        private long numberOfEdge = 0;
        private List<long> edges;
        private GraphModel graph;

        public long NumberOfVertices { get => numberOfVertices; }
        public long NumberOfEdge { get => numberOfEdge; }
        public List<long> Edges { get => edges; }
        public GraphModel Graph { get => graph; }

        private GraphLoader() 
        {
            edges = new List<long>(); 
        }

        public static GraphLoader GetInstance()
        {
            if (instance == null)
            {
                instance = new GraphLoader();
            }

            return instance;
        }

        public GraphModel LoadGraphFromConsole()
        {
            numberOfVertices = 0;
            numberOfEdge = 0;

            Console.WriteLine("------------------------------------------------------");
            Console.Write("Enter number of vertices and edges: ");
            try
            {
                var graphDetails = Console.ReadLine().Split(" ");

                if (!long.TryParse(graphDetails[0], out numberOfVertices))
                    throw new Exception("Can't load number of Vertices");

                if (!long.TryParse(graphDetails[1], out numberOfEdge))
                    throw new Exception("Can't load number of Edges");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            GraphModel graph = new GraphModel(numberOfVertices);

            try
            {
                for (int i = 0; i < numberOfEdge; i++)
                {
                    long vertexStart = 0;
                    long vertexEnd = 0;

                    Console.WriteLine("Enter edge definition: ");
                    var edgeDefinition = Console.ReadLine().Split(" ");

                    if (!long.TryParse(edgeDefinition[0], out vertexStart))
                        throw new Exception("Can't load start Vertex");

                    if (!long.TryParse(edgeDefinition[1], out vertexEnd))
                        throw new Exception("Can't load end Vertex");

                    graph.UpdateNeighbors(vertexStart, vertexEnd);

                }
            }
           catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            graph.NumberOfEdge = numberOfEdge;
            return graph;
        }

        /// <summary>
        /// File structure:
        /// 1 line - NumberOfVertices NumberOfEdges
        /// next n lines:
        /// VertexStart VertexEnd VertexStart VertexEnd...
        /// </summary>
        /// <param name="path">path to data text file</param>
        public GraphModel LoadGrpahFromFile(string path)
        {
            var lines = File.ReadAllLines(path);
            
            numberOfVertices = 0;
            numberOfEdge = 0;

            try
            {
                var graphDetails = lines[0].Split(" ");

                if (!long.TryParse(graphDetails[0], out numberOfVertices))
                    throw new Exception("Can't load number of Vertices");

                if (!long.TryParse(graphDetails[1], out numberOfEdge))
                    throw new Exception("Can't load number of Edges");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            GraphModel graph = new GraphModel(numberOfVertices);
            edges = GetEdges(lines);
            
            int vertexStartNumber = 0;
            int vertexEndNumber = 1;

            for (int i = 0; i < numberOfEdge; i++)
            {
                graph.UpdateNeighbors(edges[vertexStartNumber], edges[vertexEndNumber]);
                vertexStartNumber += 2;
                vertexEndNumber += 2;
            }

            graph.NumberOfEdge = numberOfEdge;
            return graph;
        }

        public List<long> GetEdges(string[] lines)
        {
            List<long> vertexNumber = new List<long>();

            for (int i = 0; i < lines.Length; i++)
            {
                var splitLine = lines[i].Split(" ");

                foreach (var vertex in splitLine)
                    vertexNumber.Add(long.Parse(vertex));
            }

            vertexNumber.RemoveAt(0);
            vertexNumber.RemoveAt(0);

            return vertexNumber;
        }
    }
}

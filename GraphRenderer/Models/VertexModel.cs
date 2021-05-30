using System.Collections.Generic;

namespace Graph.Models
{
    /// <summary>
    /// Vertex model class
    /// If the vertices has no neighbors, element count of list Neighbors should be 0
    /// If the vertices has no weights, element count of list Weights should be 0
    /// <param name="Id"> Vertex number </param>
    /// <param name="Neighbors"> List of vertex neighbors </param>
    /// <param name="Weights"> List of vertex weights </param>
    /// </summary>
    public class VertexModel
    {
        private long id;
        private List<VertexModel> neighbors;
        private List<long> weights;

        public long Id { get => id; set => id = value; }
        public List<VertexModel> Neighbors { get => neighbors; set => neighbors = value; }
        public List<long> Weights { get => weights; set => weights = value; }

        public VertexModel(long id, List<VertexModel> neighbors, List<long> weights)
        {
            this.id = id;
            this.neighbors = neighbors;
            this.weights = weights;
        }

        public VertexModel(long id)
        {
            this.id = id;
            this.neighbors = new List<VertexModel>();
            this.weights = new List<long>();
        }
    }
}

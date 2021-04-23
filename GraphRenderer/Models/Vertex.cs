namespace Graph.Models
{
    /// <summary>
    /// Vertex model
    /// <param name="Id"> Vertex number </param>
    /// <param name="Next"> With what vertex it is connected to </param>
    /// </summary>
    public class Vertex
    {
        private long id;
        private Vertex next;

        public long Id { get => id; set => id = value; }
        public Vertex Next { get => next; set => next = value; }
  
        public Vertex(long id, Vertex next)
        {
            this.id = id;
            this.next = next;
        }
    }
}

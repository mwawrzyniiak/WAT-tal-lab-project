using HamiltonianCycleUI;
using System;
using System.Windows.Forms;

using DFSHamiltonianCycle;
using NNHamiltonianCycle;
using HamiltonianCycleUI.Services;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Microsoft.Msagl.GraphViewerGdi.GViewer viewer;
        private Microsoft.Msagl.Drawing.Graph graph;
        private string DATA_PATH = Dictionaries.DATA_PATH;

        public Form1()
        {
            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            graph = new Microsoft.Msagl.Drawing.Graph("graph");

            //GraphGenerator.GenerateGraphToFile();

            InitializeComponent();
            //GraphBuilder();
            DFSShow();
            //NNShow();
        }

        private void NNShow()
        {
            var loader = Graph.Loader.GraphLoader.GetInstance();
            var graphFromFile = loader.LoadGrpahFromFile(DATA_PATH);

            NNHamiltonCycle nn = new NNHamiltonCycle(graphFromFile);
            label1.Text = nn.GetHamiltonCycle();

            var hamiltonCycles = nn.GetAllHamiltonCycles();

            var edges = loader.Edges;

            int startIndex = 0;
            int endIndex = 1;
            int weightIndex = 2;

            for (int i = 0; i < loader.NumberOfEdge; i++)
            {
                graph.AddEdge(edges[startIndex].ToString(), edges[endIndex].ToString()).LabelText = edges[weightIndex].ToString();
                graph.AddEdge(edges[endIndex].ToString(), edges[startIndex].ToString());
                
                startIndex += 3;
                endIndex += 3;
                weightIndex += 3;
            }
           
            if (hamiltonCycles.Count > 0)
            {
                foreach (var edge in graph.Edges)
                {
                    for (int i = 0; i < hamiltonCycles[0].Count - 1; i++)
                    {
                        if (edge.Source == hamiltonCycles[0][i].ToString() &&
                            edge.Target == hamiltonCycles[0][i + 1].ToString() ||
                            edge.Target == hamiltonCycles[0][i].ToString() &&
                            edge.Source == hamiltonCycles[0][i + 1].ToString())
                        {
                            edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                        }
                    }
                }
            }

            graph.FindNode(hamiltonCycles[0][0].ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;

            viewer.Graph = graph;
            viewer.Graph = graph;

            this.SuspendLayout();
            viewer.Dock = DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();
            this.ShowDialog();

        }
        private void DFSShow()
        {
            var loader = Graph.Loader.GraphLoader.GetInstance();
            var graphFromFile = loader.LoadGrpahFromFile(DATA_PATH);

            DFSHamiltonCycle dfs = new DFSHamiltonCycle(graphFromFile, 1);
            label1.Text = dfs.GetHamiltonCycle();
            var hamiltonCycles = dfs.GetAllHamiltonCycles();

            var edges = loader.Edges;

            int startIndex = 0;
            int endIndex = 1;

            for (int i = 0; i < loader.NumberOfEdge; i++)
            {
                graph.AddEdge(edges[startIndex].ToString(), edges[endIndex].ToString());
                graph.AddEdge(edges[endIndex].ToString(), edges[startIndex].ToString());
                startIndex += 3;
                endIndex += 3;
            }

            foreach (var edge in graph.Edges)
            {
                for(int i = 0; i < hamiltonCycles[0].Count-1; i++)
                {
                    if(edge.Source == hamiltonCycles[0][i].ToString() && 
                        edge.Target == hamiltonCycles[0][i+1].ToString() ||
                        edge.Target == hamiltonCycles[0][i].ToString() &&
                        edge.Source == hamiltonCycles[0][i + 1].ToString())
                    {
                        edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    }
                }
            }

            graph.FindNode(hamiltonCycles[0][0].ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;

            viewer.Graph = graph;
            viewer.Graph = graph;

            this.SuspendLayout();
            viewer.Dock = DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();
            this.ShowDialog();
        }

        private void GraphBuilder()
        {
            var loader = Graph.Loader.GraphLoader.GetInstance();
            var graphFromFile = loader.LoadGrpahFromFile(DATA_PATH);

            var edges = loader.Edges;

            int startIndex = 0;
            int endIndex = 1;

            for(int i = 0; i < loader.NumberOfEdge; i++)
            {
                graph.AddEdge(edges[startIndex].ToString(), edges[endIndex].ToString());
                graph.AddEdge(edges[endIndex].ToString(), edges[startIndex].ToString());
                startIndex += 2;
                endIndex += 2;
            }

            viewer.Graph = graph;
            viewer.Graph = graph;
            this.SuspendLayout();
            viewer.Dock = DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();
            this.ShowDialog();
        }

        private object GraphLoader()
        {
            throw new NotImplementedException();
        }

        /*
        private void drawExampleGraph()
        {
            graph.AddEdge("A", "1","B");
            graph.AddEdge("B", "1", "A");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            viewer.Graph = graph;
            this.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();
            this.ShowDialog();
        }*/
    }
}

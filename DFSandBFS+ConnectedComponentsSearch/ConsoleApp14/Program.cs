using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp14
{
    public class graphTemplate
    {
        Dictionary<int, List<int>> graph;
        List<bool> visitedEdges = new List<bool>();
        List<int> visitedEdgess = new List<int>();
        List<int> visitedEdgesValues = new List<int>();
        //for updated DFS
        Stack<int> stck = new Stack<int>();
        List<int> visitedVerticesNumbas = new List<int>();
        List<bool> visitedVerticesInBFS = new List<bool>();
        bool flag = false;

        int countInBFS = 1;
        int countInBFS1 = 0;
        Queue<int> q = new Queue<int>();



        public int[,] createRandomMatrix(int columns, int rows)
        {
            int[,] matrix = new int[columns, rows];
            Random r = new Random();


            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int number = r.Next(0, 10);

                    if (number == 1)
                    {
                        matrix[i, j] = 1;

                    }
                    else 
                    {
                        matrix[i, j] = 0;

                    }

                    //creating undirected graph
                    //matrix[j, i] = number;

                }
            }
            //printing this matrix
            for (int k = 0; k < matrix.GetLength(0); k++)
            {
                Console.WriteLine();
                for (int n = 0; n < matrix.GetLength(1); n++)
                {
                    Console.Write(matrix[k, n] + " ");

                }
            }
            return matrix;



        }

        public Dictionary<int, List<int>> createRandomGraph(int[,] matrix)
        {

            graph = new Dictionary<int, List<int>>();
            for (int k = 0; k < matrix.GetLength(0); k++)
            {
                visitedEdges.Add(false);
                visitedEdgess.Add(k);
                visitedEdgesValues.Add(0);
                visitedVerticesNumbas.Add(0);
                visitedVerticesInBFS.Add(false);


                //might be incorrect
                graph[k] = new List<int>();

                for (int n = 0; n < matrix.GetLength(1); n++)
                {
                    if (matrix[k, n] != 0)
                    {
                        graph[k].Add(n);
                        

                    }

                }
            }
            Console.WriteLine("\n\nconverted form");
            components = new int[graph.Count()];
            for (int a = 0; a < graph.Count(); a++)
            {
               
                Console.Write(a + " = ");
                for (int b = 0; b < graph[a].Count(); b++)
                {
                    Console.Write(graph[a][b]);
                }
                Console.WriteLine();
            }
            return graph;
            


        }
        public void solveDFS(int initialEdge)
        {

            for (int i = initialEdge; i < graph.Count; i++) 
            {
                if (visitedVerticesInBFS[i] == false) 
                {
                    Console.WriteLine("starting doing recursion at node " + i);
                    DFSUpdatedRecursive(i);
                }
            }
            Console.WriteLine("connected components amount " + visitedVerticesNumbas.Max());


        }


        public void DFSUpdatedRecursive(int initialEdge) 
        {          

            if (visitedVerticesInBFS[initialEdge] != true)
            {
                visitedVerticesNumbas[initialEdge] = countInBFS;
                stck.Push(initialEdge); 
                visitedVerticesInBFS[initialEdge] = true;
                Console.WriteLine("pushing onto stack node " + initialEdge);
            }
            for (int i = 0; i < graph[initialEdge].Count; i++) 
            {


                if (visitedVerticesInBFS[graph[initialEdge][i]] == true)
                {
                    if (visitedVerticesNumbas[initialEdge] > visitedVerticesNumbas[graph[initialEdge][i]])
                    {
                        visitedVerticesNumbas[initialEdge] = visitedVerticesNumbas[graph[initialEdge][i]];

                        Console.WriteLine("updating count in " + i + " from " + visitedVerticesNumbas[i] + " to " + visitedVerticesNumbas[initialEdge]);
                    }

                }
                else 
                {
                    Console.WriteLine("mooving onto node" + graph[initialEdge][i]);
                    //DFSUpdatedRecursive(graph[initialEdge][i]);
                    return;
                }

              /*  if (visitedVerticesInBFS[graph[initialEdge][i]] != true) 
                {
                    if (visitedVerticesNumbas[i] < count ) 
                    {
                        visitedVerticesNumbas[i] = visitedVerticesNumbas[initialEdge];
                       
                    }

                    Console.WriteLine("mooving onto node" + graph[initialEdge][i]);
                    DFSUpdatedRecursive(graph[initialEdge][i]);
                }*/
            }
            var node = stck.Pop();

            if (stck.Count != 0)
            {
                DFSUpdatedRecursive(stck.Peek());
            }
            if (stck.Count == 0)
            {
                Console.WriteLine("increasing the count on 1 " + countInBFS);
                countInBFS++;
                return;
                flag = true;

                    
            }



        }

        public void BFSReqursive()
        {
            for(int i =0;i<graph.Count();i++)
            {
                if (visitedEdges[i] == false)
                {
                    solveForBFSRecursive(visitedEdgess[i]);
                }
            }
            Console.WriteLine("connectivity components = "  + countInBFS1);



        }
        public void solveForBFSRecursive(int listOfNodes)   
        {
            Console.WriteLine("Starting bfs on vertice " + listOfNodes);
            Queue<int> q = new Queue<int>();
            q.Enqueue(listOfNodes);
            visitedEdges[listOfNodes] = true;
            visitedEdgesValues[listOfNodes] = countInBFS1;
            while (q.Count != 0)
            {
                var node = q.Dequeue();
                Console.WriteLine("Starting additional bfs on vertice " + node);
                var neighbours = graph[node];
                for (int i = 0; i < neighbours.Count; i++) 
                {
                    if (visitedEdges[neighbours[i]] == true) 
                    {
                        visitedEdgesValues[listOfNodes] = visitedEdgesValues[neighbours[i]];


                    }
                
                    if (visitedEdges[neighbours[i]] == false)
                    {
                        Console.WriteLine("adding to queue " + neighbours[i]);
                        q.Enqueue(neighbours[i]);
                        visitedEdges[neighbours[i]] = true;
                    }
                } 
            }
            //we might do it using the set amount because set elements are not equal to each other

            countInBFS1 = visitedEdgesValues.Max() +1;

        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            graphTemplate gr = new graphTemplate();
            int[,] matrix = gr.createRandomMatrix(5,5);
            gr.createRandomGraph(matrix);
            gr.BFSReqursive();

        }
    }
}

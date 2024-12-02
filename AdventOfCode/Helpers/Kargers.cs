using System;
using System.IO;
using System.Collections.Generic;
using AdventOfCode.Models;

namespace AdventOfCode.Helpers
{
    public class Kargers
    {
        public int MinCut(Graph<int> originalGraph, int samplingCount = 0)
        {
            if (originalGraph is null)
                return default;

            var repeatCount = samplingCount;

            if (repeatCount == 0)
            {
                var numberOfVertices = originalGraph.Vertices.ToList().Count;
                repeatCount = (int)((2 ^ numberOfVertices) * Math.Log(numberOfVertices));
            }

            var originalVertices = originalGraph.Vertices.ToList();
            var minCuts = new List<int>(samplingCount);

            // It's a randomized algorithm so we should run a few iterations to achieve the results as close as possible to the optimum.
            while (repeatCount > 0)
            {
                var vertices = new Dictionary<int, List<int>>();
                // Copy all vertices to a new dictionary with indexes as a key and adjacent vertices as values.
                for (var j = 0; j < originalVertices.Count; j++)
                {
                    var original = originalVertices[j];
                    vertices.Add(j, new List<int>(original));
                }

                // Keep contracting edges until only two left.
                while (vertices.Count > 2)
                {
                    // Pick random edge.
                    var (from, to) = GetRandomEdge(vertices);

                    // Iterate over adjacent vertices of 'to' vertex. We are going to conflate it with 'from' vertex.
                    for (var i = 0; i < vertices[to].Count; i++)
                    {
                        var vertex = vertices[to][i];

                        // To avoid self-loops. They aren't allowed according to Karger's algorithm.
                        if (vertex != from)
                        {
                            vertices[from].Add(vertex);
                        }
                        // Remove edge to conflated vertex from adjacent vertex.
                        vertices[vertex].Remove(to);
                        // Add edge to 'from' vertex since 'to' vertex was conflated into it.
                        if (vertex != from)
                        {
                            vertices[vertex].Add(from);
                        }
                    }
                    // We don't need conflated vertex anymore.
                    vertices.Remove(to);
                }

                // Count of vertices from the first node is the min cut. 
                minCuts.Add(vertices.First().Value.Count);
                repeatCount--;
            }

            return minCuts.Min();
        }

        // TODO: There is a better way to pick a random edge.
        private static (int from, int to) GetRandomEdge(Dictionary<int, List<int>> vertices)
        {
            var r = new Random();
            var from = -1;
            var to = -1;

            while (from < 0 || to < 0)
            {
                from = r.Next(vertices.Count);
                if (vertices.Keys.Any(arg => arg == from))
                {
                    to = r.Next(vertices[from].Count);

                    if (vertices[from][to] != from)
                    {
                        to = vertices[from][to];
                    }
                }
            }

            return (from, to);
        }
    }
}

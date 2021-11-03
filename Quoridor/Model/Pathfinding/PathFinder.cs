using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Quoridor.Model.Common;

namespace Quoridor.Model.Pathfinding
{
    public static class PathFinder
    {
        private const int NeighboursDistance = 1;

        public static List<Coordinates> FindPath(Coordinates start, Coordinates goal, int[,] field, int amountOfRows, int amountOfColumns)
        { 
            var closedSet = new Collection<Node>();
            var openSet = new Collection<Node>();

            Node startNode = new Node()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = CalculateApproximateDistance(start, goal)
            };
            openSet.Add(startNode);
            
            while (openSet.Count > 0)
            {
                Node currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();

                if (currentNode.Position.Equals(goal))
                {
                    return GetPathForNode(currentNode);
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (Node neighbourNode in GetNodeNeighbours(currentNode, goal, field, amountOfRows, amountOfColumns))
                {
                    if (closedSet.Count(node => node.Position.Equals(neighbourNode.Position)) > 0)
                    {
                        continue;
                    }

                    Node openNode = openSet.FirstOrDefault(node => node.Position.Equals(neighbourNode.Position));
                    if (openNode == null)
                    {
                        openSet.Add(neighbourNode);
                    }
                    else if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }

            return null;
        }
        
        private static int CalculateApproximateDistance(Coordinates start, Coordinates goal)
        {
            int rowDistance = Math.Abs(start.Row - goal.Row);
            int columnDistance = Math.Abs(start.Column - goal.Column);
            
            return rowDistance + columnDistance;
        }
        private static Collection<Node> GetNodeNeighbours(Node node, Coordinates goal, int[,] field, int amountOfRows, int amountOfColumns)
        {
            var result = new Collection<Node>();

            int nodeIndex = FieldConverter.ToIndex(node.Position, amountOfColumns);
            for (int i = 0; i < field.GetLength(1); i++)
            {
                if (field[nodeIndex, i] != 1)
                {
                    continue;
                }
                
                Coordinates neighbourCoordinates = FieldConverter.ToCoordinates(i, amountOfRows, amountOfColumns);
                var neighbourNode = new Node
                {
                    Position = neighbourCoordinates,
                    CameFrom = node,
                    PathLengthFromStart = node.PathLengthFromStart + NeighboursDistance,
                    HeuristicEstimatePathLength = CalculateApproximateDistance(neighbourCoordinates, goal)
                };
                result.Add(neighbourNode);
            }
            
            return result;
        }

        private static List<Coordinates> GetPathForNode(Node node)
        {
            var result = new List<Coordinates>();
            Node currentNode = node;

            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }

            result.Reverse();
            return result;
        }
    }
}

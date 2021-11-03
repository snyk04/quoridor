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

                foreach (Node neighbourNode in GetNodeNeighbours(currentNode, field, amountOfRows, amountOfColumns))
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
            int rowDistance = Math.Abs(start.row - goal.row);
            int columnDistance = Math.Abs(start.column - goal.column);
            
            return rowDistance + columnDistance;
        }
        private static Collection<Node> GetNodeNeighbours(Node node, int[,] field, int amountOfRows, int amountOfColumns)
        {
            var result = new Collection<Node>();

            Coordinates[] nodeNeighbours =
            {
                new(node.Position.row + 1, node.Position.column),
                new(node.Position.row - 1, node.Position.column),
                new(node.Position.row, node.Position.column + 1),
                new(node.Position.row, node.Position.column - 1)
            };
            
            foreach (Coordinates neighbour in nodeNeighbours)
            {
                TryToAddNeighbourToNode(neighbour, node, field, result, amountOfRows, amountOfColumns);
            }

            return result;
        }

        private static void TryToAddNeighbourToNode(Coordinates neighbour, Node node, int[,] field, ICollection<Node> result, int amountOfRows, int amountOfColumns)
        {
            if (!IsNodeReal(neighbour, amountOfRows, amountOfColumns))
            {
                return;
            }

            if (!IsWayBetweenNodes(neighbour, node.Position, field, amountOfColumns))
            {
                return;
            }

            var neighbourNode = new Node
            {
                Position = neighbour,
                CameFrom = node,
                PathLengthFromStart = node.PathLengthFromStart + NeighboursDistance,
                HeuristicEstimatePathLength = CalculateApproximateDistance(neighbour, neighbour)
            };
            result.Add(neighbourNode);
        }

        private static bool IsNodeReal(Coordinates node, int amountOfRows, int amountOfColumns)
        {
            return node.row < amountOfRows
                   & node.row >= 0
                   & node.column < amountOfColumns
                   & node.column >= 0; 
        }
        private static bool IsWayBetweenNodes(Coordinates node1, Coordinates node2, int[,] field, int amountOfColumns)
        {
            return field[FieldConverter.ToIndex(node1, amountOfColumns), FieldConverter.ToIndex(node2, amountOfColumns)] != 0 
                   || field[FieldConverter.ToIndex(node2, amountOfColumns), FieldConverter.ToIndex(node1, amountOfColumns)] != 0;
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

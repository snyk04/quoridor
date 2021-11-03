using Quoridor.Model.Common;

namespace Quoridor.Model.Pathfinding
{
    public class Node
    {
        public Coordinates Position { get; set; }
        public int PathLengthFromStart { get; set; }
        public Node CameFrom { get; set; }
        public int HeuristicEstimatePathLength { get; set; }
        public int EstimateFullPathLength => PathLengthFromStart + HeuristicEstimatePathLength;
    }
}
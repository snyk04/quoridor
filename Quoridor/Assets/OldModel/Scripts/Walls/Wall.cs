using System.Collections.Generic;
using Quoridor.OldModel.Cells;

namespace Quoridor.OldModel.Walls
{
    public class Wall
    {
        public List<CellPair> BlockedCellPairs { get; }
        public List<Coordinates> OverlappedWalls { get; }

        public Wall(List<CellPair> blockedCellPairs, List<Coordinates> overlappedWalls)
        {
            BlockedCellPairs = blockedCellPairs;
            OverlappedWalls = overlappedWalls;
        }
    }
}

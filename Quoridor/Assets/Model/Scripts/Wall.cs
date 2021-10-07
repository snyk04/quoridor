using System.Collections.Generic;
using Quoridor.Model.Cells;

namespace Quoridor.Model
{
    public class Wall
    {
        public List<CellPair> BlockedCellPairs { get; }
        public List<CellCoordinates> OverlappedWalls { get; }

        public Wall(List<CellPair> blockedCellPairs, List<CellCoordinates> overlappedWalls)
        {
            BlockedCellPairs = blockedCellPairs;
            OverlappedWalls = overlappedWalls;
        }
    }
}

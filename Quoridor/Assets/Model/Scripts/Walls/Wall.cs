using Quoridor.Model.Cells;
using Quoridor.Model.Common;

namespace Quoridor.Model.Walls
{
    public class Wall
    {
        public CellPair[] BlockedCellPairs { get; }
        public Coordinates[] OverlappedWalls { get; }

        public Wall(CellPair[] blockedCellPairs, Coordinates[] overlappedWalls)
        {
            BlockedCellPairs = blockedCellPairs;
            OverlappedWalls = overlappedWalls;
        }
    }
}

using System.Collections.Generic;
using Quoridor.Model.Cells;

namespace Quoridor.Model.Players
{
    public abstract class Bot : Player
    {
        public CellCoordinates CellToMove { get; protected set; }
        public CellCoordinates WallToPlace { get; protected set; }

        protected Bot(int startAmountOfWalls, CellCoordinates startPosition) : base(startAmountOfWalls, startPosition)
        {
        }

        public abstract MoveType MakeMove(List<CellCoordinates> availableMoves, List<CellCoordinates> wallsThatCanBePlaced);
    }
}

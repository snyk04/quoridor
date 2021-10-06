using System.Collections.Generic;
using Quoridor.Model.Cells;

namespace Quoridor.Model.Players
{
    public abstract class Bot : Player
    {
        public CellCoordinates CellToMove { get; protected set; }

        protected Bot(CellCoordinates startPosition) : base(startPosition)
        {
        }

        public abstract MoveType MakeMove(List<CellCoordinates> availableMoves);
    }
}

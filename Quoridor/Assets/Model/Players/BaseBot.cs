using System.Collections.Generic;

namespace Quoridor.Model.Players
{
    public abstract class BaseBot : Player
    {
        public CellCoordinates CellToMove { get; protected set; }

        protected BaseBot(CellCoordinates startPosition) : base(startPosition)
        {
        }

        public abstract MoveType MakeMove(List<CellCoordinates> availableMoves);
    }
}

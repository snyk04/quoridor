using System.Collections.Generic;
using Quoridor.Model.Cells;

namespace Quoridor.Model.Players
{
    public abstract class Bot : Player
    {
        public Coordinates CellToMove { get; protected set; }
        public Coordinates WallToPlace { get; protected set; }

        protected Bot(int startAmountOfWalls, Coordinates startPosition) : base(startAmountOfWalls, startPosition) { }

        public abstract MoveType MakeMove(List<Coordinates> availableMoves, List<Coordinates> wallsThatCanBePlaced);
    }
}

using Quoridor.Model.Cells;

namespace Quoridor.Model.Players
{
    public class Player
    {
        private readonly int _defaultAmountOfWalls;

        public int AmountOfWalls { get; private set; }
        public Coordinates CurrentCell { get; private set; }
        
        public Player(int startAmountOfWalls, Coordinates startPosition)
        {
            _defaultAmountOfWalls = startAmountOfWalls;
            
            AmountOfWalls = startAmountOfWalls;
            CurrentCell = startPosition;
        }

        public void MoveToCell(Coordinates cell)
        {
            CurrentCell = cell;
        }

        public void PlaceWall()
        {
            AmountOfWalls -= 1;
        }
        public void ResetAmountOfWalls()
        {
            AmountOfWalls = _defaultAmountOfWalls;
        }
    }
}

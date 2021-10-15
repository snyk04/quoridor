using System;
using Quoridor.Model.Common;

namespace Quoridor.Model.Cells
{
    public readonly struct CellPair
    {
        public readonly Coordinates firstCell;
        public readonly Coordinates secondCell;

        public CellPair(Coordinates firstCell, Coordinates secondCell)
        {
            this.firstCell = firstCell;
            this.secondCell = secondCell;
        }
        
        public Coordinates this[int index]
        {
            get
            {
                return index switch
                {
                    0 => firstCell,
                    1 => secondCell,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public bool Equals(CellPair cellPair)
        {
            // TODO : caused stackOverflow when BotVsBot
            return firstCell.Equals(cellPair.firstCell) && secondCell.Equals(cellPair.secondCell)
                || firstCell.Equals(cellPair.secondCell) && secondCell.Equals(cellPair.firstCell);
        }
    }
}

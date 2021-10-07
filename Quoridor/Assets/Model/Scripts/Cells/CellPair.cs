using System;
using UnityEngine;

namespace Quoridor.Model.Cells
{
    public readonly struct CellPair
    {
        public readonly CellCoordinates _firstCellCoordinates;
        public readonly CellCoordinates _secondCellCoordinates;

        public CellPair(CellCoordinates firstCellCoordinates, CellCoordinates secondCellCoordinates)
        {
            _firstCellCoordinates = firstCellCoordinates;
            _secondCellCoordinates = secondCellCoordinates;
        }
        
        public CellCoordinates this[int index]
        {
            get
            {
                return index switch
                {
                    0 => _firstCellCoordinates,
                    1 => _secondCellCoordinates,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public bool Contains(CellCoordinates cellCoordinates)
        {
            return cellCoordinates.Equals(_firstCellCoordinates) | cellCoordinates.Equals(_secondCellCoordinates);
        }
    }
}

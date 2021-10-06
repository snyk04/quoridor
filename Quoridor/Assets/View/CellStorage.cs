using System.Collections.Generic;
using Quoridor.Model;
using UnityEngine;

namespace Quoridor.View
{
    public class CellStorage : MonoBehaviour
    {
        [SerializeField] private List<CellVisual> _cells;
        public IEnumerable<CellVisual> Cells => _cells;
        
        private int _amountOfColumns;

        private void Awake()
        {
            _amountOfColumns = CellsManager.AmountOfColumns;
        }

        private int TwoDimensionalToOneDimensional(CellCoordinates cellCoordinates)
        {
            return cellCoordinates.row * _amountOfColumns + cellCoordinates.column;
        }
        public CellVisual GetCell(CellCoordinates cellCoordinates)
        {
            int cellIndex = TwoDimensionalToOneDimensional(cellCoordinates);
            return _cells[cellIndex];
        }
    }
}

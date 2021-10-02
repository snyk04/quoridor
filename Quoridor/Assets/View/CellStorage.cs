using System.Collections.Generic;
using Quoridor.Model;
using UnityEngine;

namespace Quoridor.View
{
    public class CellStorage : MonoBehaviour
    {
        // TODO : get that info from model!!!
        private const int AmountOfRows = 9;
        private const int AmountOfColumns = 9;
        
        [SerializeField] private List<CellVisual> _cells;

        public List<CellVisual> Cells => _cells;

        public int TwoDimensionalToOneDimensional(CellCoordinates cellCoordinates)
        {
            return cellCoordinates.row * AmountOfColumns + cellCoordinates.column;
        }
    }
}

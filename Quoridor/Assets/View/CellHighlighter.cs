using System.Collections.Generic;
using Quoridor.Model;
using UnityEngine;

namespace Quoridor.View
{
    public class CellHighlighter : MonoBehaviour
    {
        [SerializeField] private CellStorage _cellStorage;

        public void UnhighlightAllCells()
        {
            foreach (CellVisual cell in _cellStorage.Cells)
            {
                cell.UnHighlight();
            }
        }
        public void HighlightCells(IEnumerable<CellCoordinates> cellCoordinatesArray)
        {
            UnhighlightAllCells();
            
            foreach (CellCoordinates cellCoordinates in cellCoordinatesArray)
            {
                int index = _cellStorage.TwoDimensionalToOneDimensional(cellCoordinates);
                _cellStorage.Cells[index].Highlight();
            }
        }
    }
}

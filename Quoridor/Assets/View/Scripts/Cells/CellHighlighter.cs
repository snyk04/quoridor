using System.Collections.Generic;
using Quoridor.Model.Cells;
using UnityEngine;

namespace Quoridor.View.Cells
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
                _cellStorage.GetCell(cellCoordinates).Highlight();
            }
        }
    }
}

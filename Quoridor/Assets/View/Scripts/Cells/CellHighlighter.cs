using System.Collections.Generic;
using Quoridor.NewModel.Common;
using UnityEngine;

namespace Quoridor.View.Cells
{
    public class CellHighlighter : MonoBehaviour
    {
        [SerializeField] private CellStorage _cellStorage;

        public void UnhighlightAllCells()
        {
            foreach (CellVisual cell in _cellStorage)
            {
                cell.Unhighlight();
            }
        }
        public void HighlightCells(IEnumerable<Coordinates> cellCoordinatesArray)
        {
            UnhighlightAllCells();
            
            foreach (Coordinates cellCoordinates in cellCoordinatesArray)
            {
                CellVisual cell = _cellStorage[cellCoordinates];
                cell.Highlight();
            }
        }
    }
}

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
            Debug.Log("highlight");
            UnhighlightAllCells();
            
            foreach (CellCoordinates cellCoordinates in cellCoordinatesArray)
            {
                _cellStorage.GetCell(cellCoordinates).Highlight();
            }
        }
    }
}

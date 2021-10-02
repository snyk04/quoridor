using System.Collections.Generic;
using Quoridor.Model;
using UnityEngine;

namespace Quoridor.View
{
    public class CellHighlighter : MonoBehaviour
    {
        [SerializeField] private CellStorage _cellStorage;
        
        public void HighlightCells(IEnumerable<CellCoordinates> cellCoordinatesArray)
        {
            foreach (CellVisual cell in _cellStorage.Cells)
            {
                cell.UnHighlight();
            }
            
            foreach (CellCoordinates cellCoordinates in cellCoordinatesArray)
            {
                int index = _cellStorage.TwoDimensionalToOneDimensional(cellCoordinates);
                _cellStorage.Cells[index].Highlight();
            }
        }
    }
}

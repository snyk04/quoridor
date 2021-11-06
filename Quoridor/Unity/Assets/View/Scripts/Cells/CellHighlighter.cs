using System.Collections.Generic;
using Quoridor.Model.Common;
using UnityEngine;

namespace Quoridor.View.Cells
{
    public sealed class CellHighlighter : MonoBehaviour
    {
        [SerializeField] private ViewCommunication _view;

        public void UnhighlightAllCells()
        {
            foreach (CellVisual cell in _view.CellStorage)
            {
                cell.Unhighlight();
            }
        }
        public void HighlightCells(IEnumerable<Coordinates> cellCoordinatesArray)
        {
            UnhighlightAllCells();

            foreach (Coordinates cellCoordinates in cellCoordinatesArray)
            {
                CellVisual cell = _view.CellStorage[cellCoordinates];
                cell.Highlight();
            }
        }
    }
}

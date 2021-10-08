﻿using System.Collections.Generic;
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
                cell.Unhighlight();
            }
        }
        public void HighlightCells(IEnumerable<CellCoordinates> cellCoordinatesArray)
        {
            UnhighlightAllCells();
            
            foreach (CellCoordinates cellCoordinates in cellCoordinatesArray)
            {
                CellVisual cell = _cellStorage.GetCell(cellCoordinates);
                cell.Highlight();
            }
        }
    }
}

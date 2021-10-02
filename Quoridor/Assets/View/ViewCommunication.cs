using System.Collections.Generic;
using Quoridor.Model;
using UnityEngine;

namespace Quoridor.View
{
    public class ViewCommunication : MonoBehaviour, IView
    {
        [SerializeField] private CellHighlighter _cellHighlighter;
        [SerializeField] private PawnMover _pawnMover;
        
        public void HighlightCells(IEnumerable<CellCoordinates> cellCoordinatesArray)
        {
            _cellHighlighter.HighlightCells(cellCoordinatesArray);
        }
        public void MovePawnToCell(PawnType pawnType, CellCoordinates cellCoordinates)
        {
            _pawnMover.MovePawnToCell(pawnType, cellCoordinates);
        }
    }
}

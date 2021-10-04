using System.Collections.Generic;
using Quoridor.Model;
using Quoridor.Model.Players;
using UnityEngine;

namespace Quoridor.View
{
    public class ViewCommunication : MonoBehaviour, IView
    {
        [SerializeField] private CellHighlighter _cellHighlighter;
        [SerializeField] private PawnMover _pawnMover;

        public void UnhighlightAllCells()
        {
            _cellHighlighter.UnhighlightAllCells();
        }
        public void HighlightCells(IEnumerable<CellCoordinates> cellCoordinatesArray)
        {
            _cellHighlighter.HighlightCells(cellCoordinatesArray);
        }
        public void MovePawnToCell(PlayerType playerType, CellCoordinates cellCoordinates)
        {
            _pawnMover.MovePawnToCell(playerType, cellCoordinates);
        }

        public void ShowVictory(PlayerType playerType)
        {
            Debug.Log(playerType + " won!");
        }
    }
}

﻿using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Players;
using Quoridor.View.Cells;
using Quoridor.View.Walls;
using UnityEngine;

namespace Quoridor.View
{
    public class ViewCommunication : MonoBehaviour, IView
    {
        [Header("Components")]
        [SerializeField] private CellHighlighter _cellHighlighter;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private WallPlacer _wallPlacer;
        
        public void HighlightCells(IEnumerable<CellCoordinates> cellCoordinatesArray)
        {
            _cellHighlighter.HighlightCells(cellCoordinatesArray);
        }
        public void MovePlayerToCell(PlayerType playerType, CellCoordinates cellCoordinates)
        {
            _playerMover.MovePlayerToCell(playerType, cellCoordinates);
        }
        public void PlaceWall(CellCoordinates wallCoordinates)
        {
            _wallPlacer.PlaceWall(wallCoordinates);
        }

        public void EndGame(PlayerType winner)
        {
            _cellHighlighter.UnhighlightAllCells();
            Debug.Log(winner + " won!");
        }
    }
}

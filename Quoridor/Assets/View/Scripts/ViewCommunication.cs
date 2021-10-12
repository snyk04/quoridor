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
        [SerializeField] private SoundPlayer _backgroundMusicPlayer;
        [SerializeField] private CellHighlighter _cellHighlighter;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private VictoryManager _victoryManager;
        [SerializeField] private WallPlacer _wallPlacer;

        private void Start()
        {
            _backgroundMusicPlayer.Play();
        }

        public void HighlightCells(IEnumerable<Coordinates> cellCoordinatesArray)
        {
            _cellHighlighter.HighlightCells(cellCoordinatesArray);
        }
        public void MovePlayerToCell(PlayerType playerType, Coordinates cellCoordinates)
        {
            _playerMover.MovePlayerToCell(playerType, cellCoordinates);
        }
        public void PlaceWall(Coordinates wallCoordinates, IEnumerable<Coordinates> overlappedWalls)
        {
            _wallPlacer.PlaceWall(wallCoordinates, overlappedWalls);
        }

        public void EndGame(PlayerType winner)
        {
            _victoryManager.ShowVictory(winner);
        }
    }
}

using System.Collections.Generic;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;
using Quoridor.View.Cells;
using Quoridor.View.UserInterface;
using Quoridor.View.Walls;
using UnityEngine;

namespace Quoridor.View
{
    public class ViewCommunication : MonoBehaviour, IView
    {
        [Header("Components")] 
        [SerializeField] private AmountOfWallsUpdater _amountOfWallsUpdater;
        [SerializeField] private SoundPlayer _backgroundMusicPlayer;
        [SerializeField] private CellHighlighter _cellHighlighter;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private VictoryManager _victoryManager;
        [SerializeField] private WallController _wallController;

        private void Start()
        {
            _backgroundMusicPlayer.Play();
        }

        public void ShowAvailableMoves(IEnumerable<Coordinates> cells)
        {
            _cellHighlighter.HighlightCells(cells);
        }

        public void ShowAvailableWalls(IEnumerable<Coordinates> walls)
        {
            _wallController.EnableWalls(walls);
        }

        public void MovePlayerToCell(PlayerType playerType, Coordinates cell)
        {
            _playerMover.MovePlayerToCell(playerType, cell);
        }
        public void PlaceWall(Player player, Coordinates wall)
        {
            _wallController.PlaceWall(wall);
            _amountOfWallsUpdater.UpdateCounter(player);
        }

        public void EndGame(PlayerType winner)
        {
            _victoryManager.ShowVictory(winner);
        }
    }
}

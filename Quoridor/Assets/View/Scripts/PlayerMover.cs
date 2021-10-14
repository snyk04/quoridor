using System;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;
using Quoridor.View.Audio;
using Quoridor.View.Cells;
using UnityEngine;

namespace Quoridor.View
{
    public class PlayerMover : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CellStorage _cellStorage;
        [SerializeField] private RandomSoundPlayer _playerSoundPlayer;
        
        [Header("Objects")]
        [SerializeField] private Transform _firstPlayer;
        [SerializeField] private Transform _secondPlayer;

        private Transform GetPlayer(PlayerType playerType)
        {
            return playerType switch
            {
                PlayerType.First => _firstPlayer,
                PlayerType.Second => _secondPlayer,
                _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
            };
        }
        public void MovePlayerToCell(PlayerType playerType, Coordinates cellCoordinates)
        {
            Transform player = GetPlayer(playerType);

            CellVisual cell = _cellStorage[cellCoordinates];
            Vector3 newPosition = cell.transform.position;
            player.position = newPosition;
            
            _playerSoundPlayer.PlayNext();
        }
    }
}

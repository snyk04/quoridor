using System;
using Quoridor.Model.Cells;
using Quoridor.Model.Players;
using Quoridor.View.Cells;
using UnityEngine;

namespace Quoridor.View
{
    public class PlayerMover : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CellStorage _cellStorage;
        
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
        }
    }
}

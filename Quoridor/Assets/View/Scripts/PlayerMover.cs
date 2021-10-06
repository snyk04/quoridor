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

        public void MovePlayerToCell(PlayerType playerType, CellCoordinates cellCoordinates)
        {
            Transform player = playerType switch
            {
                PlayerType.First => _firstPlayer,
                PlayerType.Second => _secondPlayer,
                _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
            };

            Vector3 playerNewPosition = _cellStorage.GetCell(cellCoordinates).transform.position;
            player.position = playerNewPosition;
        }
    }
}

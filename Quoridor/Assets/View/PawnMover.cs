using System;
using Quoridor.Model;
using Quoridor.Model.Players;
using UnityEngine;

namespace Quoridor.View
{
    public class PawnMover : MonoBehaviour
    {
        [SerializeField] private Transform _blackPawn;
        [SerializeField] private Transform _whitePawn;

        [SerializeField] private CellStorage _cellStorage;
        
        public void MovePawnToCell(PlayerType playerType, CellCoordinates cellCoordinates)
        {
            Transform pawn = playerType switch
            {
                PlayerType.Black => _blackPawn,
                PlayerType.White => _whitePawn,
                _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
            };

            int index = _cellStorage.TwoDimensionalToOneDimensional(cellCoordinates);
            Vector3 pawnNewPosition = _cellStorage.Cells[index].transform.position;
            pawn.position = pawnNewPosition;
        }
    }
}

using System;
using Quoridor.Model;
using UnityEngine;

namespace Quoridor.View
{
    public class PawnMover : MonoBehaviour
    {
        [SerializeField] private Transform _blackPawn;
        [SerializeField] private Transform _whitePawn;

        [SerializeField] private CellStorage _cellStorage;
        
        public void MovePawnToCell(PawnType pawnType, CellCoordinates cellCoordinates)
        {
            Transform pawn = pawnType switch
            {
                PawnType.Black => _blackPawn,
                PawnType.White => _whitePawn,
                _ => throw new ArgumentOutOfRangeException(nameof(pawnType), pawnType, null)
            };

            int index = _cellStorage.TwoDimensionalToOneDimensional(cellCoordinates);
            Vector3 pawnNewPosition = _cellStorage.Cells[index].transform.position;
            pawn.position = pawnNewPosition;
        }
    }
}

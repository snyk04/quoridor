using System;
using System.Collections.Generic;
using Quoridor.Model;
using UnityEngine;

namespace Quoridor.View
{
    public class CellStorage : MonoBehaviour
    {
        [SerializeField] private List<CellVisual> _cells;
        public List<CellVisual> Cells => _cells;
        
        private int _amountOfColumns;

        private void Awake()
        {
            _amountOfColumns = ModelCommunication.AmountOfColumns;
        }

        public int TwoDimensionalToOneDimensional(CellCoordinates cellCoordinates)
        {
            return cellCoordinates.row * _amountOfColumns + cellCoordinates.column;
        }
    }
}

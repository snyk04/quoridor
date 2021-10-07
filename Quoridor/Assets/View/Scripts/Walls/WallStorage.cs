using System.Collections.Generic;
using Quoridor.Controller.Buttons;
using Quoridor.Model.Cells;
using UnityEngine;

namespace Quoridor.View.Walls
{
    // TODO : maybe generic class for storage?
    [ExecuteInEditMode]
    public class WallStorage : MonoBehaviour
    {
        [SerializeField] private List<WallVisual> _walls;

        private int _amountOfColumns;
        
        private void Awake()
        {
            // TODO : take from CellsManager
            _amountOfColumns = 8;
        }

        private void OnRenderObject()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _walls[i * 8 + j].GetComponent<WallButton>()._wallCoordinates = new Vector2Int(i, j);
                }
            }
        }

        private int TwoDimensionalToOneDimensional(CellCoordinates cellCoordinates)
        {
            return cellCoordinates.row * _amountOfColumns + cellCoordinates.column;
        }
        public WallVisual GetWall(CellCoordinates cellCoordinates)
        {
            int wallIndex = TwoDimensionalToOneDimensional(cellCoordinates);
            return _walls[wallIndex];
        }
    }
}
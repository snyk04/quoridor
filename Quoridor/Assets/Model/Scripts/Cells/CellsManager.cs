using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quoridor.Model.Cells
{
    // TODO : rename to FieldManager
    public class CellsManager
    {
        private readonly ModelCommunication _model;

        public const int AmountOfRows = 9;
        public const int AmountOfColumns = 9;

        public const int WallsAmountOfRows = 16;
        public const int WallsAmountOfColumns = 16;

        public Cell[,] Cells { get; }
        
        public Wall[,] Walls { get; }
        public List<CellPair> BlockedCellPairs { get; }
        public List<CellCoordinates> WallsThatCanBePlaced { get; }
        
        public CellsManager(ModelCommunication model)
        {
            _model = model;

            Cells = new Cell[AmountOfRows, AmountOfColumns];
            Walls = new Wall[16, 8];
            BlockedCellPairs = new List<CellPair>();

            WallsThatCanBePlaced = new List<CellCoordinates>();

            InitializeCellField();
            InitializeWallField();
        }

        private void InitializeCellField()
        {
            for (int i = 0; i < AmountOfRows; i++)
            {
                for (int j = 0; j < AmountOfColumns; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }
        }
        private void InitializeWallField()
        {
            for (int i = 0; i < Walls.GetLength(0); i++)
            {
                for (int j = 0; j < Walls.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        Walls[i, j] = new Wall(
                            new List<CellPair>
                            {
                                new CellPair(new CellCoordinates(i / 2, j), new CellCoordinates(i / 2, j + 1)),
                                new CellPair(new CellCoordinates(i / 2 + 1, j), new CellCoordinates(i / 2 + 1, j + 1))
                            },
                            new List<CellCoordinates>
                            {
                                new CellCoordinates(Math.Max(i - 2, 0), j), new CellCoordinates(Math.Min(i + 2, Walls.GetLength(0) - 1), j),
                                new CellCoordinates(Math.Min(i + 1, Walls.GetLength(0) - 1), j)
                            });
                    }
                    else
                    {
                        Walls[i, j] = new Wall(
                            new List<CellPair>
                            {
                                new CellPair(new CellCoordinates(i / 2, j), new CellCoordinates(i / 2 + 1, j)),
                                new CellPair(new CellCoordinates(i / 2, j + 1), new CellCoordinates(i / 2 + 1, j + 1))
                            },
                            new List<CellCoordinates>
                            {
                                new CellCoordinates(i, j - 1), new CellCoordinates(i, j + 1), new CellCoordinates(i - 1, j)
                            });
                    }
                    
                    WallsThatCanBePlaced.Add(new CellCoordinates(i, j));
                }
            }
        }

        public bool CheckIfWallCanBePlaced(CellCoordinates wallCoordinates)
        {
            return !WallsThatCanBePlaced.Contains(wallCoordinates);
        }
        public void PlaceWall(CellCoordinates wallCoordinates)
        {
            Wall wall = Walls[wallCoordinates.row, wallCoordinates.column];

            BlockedCellPairs.AddRange(wall.BlockedCellPairs);

            WallsThatCanBePlaced.Remove(wallCoordinates);
            foreach (CellCoordinates overlappedWallCoordinates in wall.OverlappedWalls)
            {
                WallsThatCanBePlaced.Remove(overlappedWallCoordinates);
            }
        }
        public void DestroyWall(CellCoordinates wallCoordinates)
        {
            Wall wall = Walls[wallCoordinates.row, wallCoordinates.column];
            foreach (CellPair cellPair in wall.BlockedCellPairs)
            {
                BlockedCellPairs.Remove(cellPair);
            }
            
            WallsThatCanBePlaced.Add(wallCoordinates);
            foreach (CellCoordinates overlappedWallCoordinates in wall.OverlappedWalls)
            {
                WallsThatCanBePlaced.Add(overlappedWallCoordinates);
            }
        }

        public bool CheckIfCellIsReal(CellCoordinates cellCoordinates)
        {
            return cellCoordinates.row < AmountOfRows
                   & cellCoordinates.row >= 0
                   & cellCoordinates.column < AmountOfColumns
                   & cellCoordinates.column >= 0;
        }
        public bool CheckIfCellIsBusy(CellCoordinates cellCoordinates)
        {
            return Cells[cellCoordinates.row, cellCoordinates.column].IsBusy;
        }

        public bool CheckIfWallIsBetweenCells(CellCoordinates firstCell, CellCoordinates secondCell)
        {
            bool isThereWall = false;
            foreach (CellPair blockedCellPair in BlockedCellPairs)
            {
                isThereWall |= blockedCellPair.Contains(firstCell) & blockedCellPair.Contains(secondCell);
            }

            return isThereWall;
        }
    }
}

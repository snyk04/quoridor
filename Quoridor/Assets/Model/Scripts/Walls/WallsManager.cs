using System;
using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.Model.Walls
{
    public sealed class WallsManager
    { 
        public const int AmountOfRows = 16;
        public const int AmountOfColumns = 8;
        
        public Wall[,] Walls { get; }
        
        public List<CellPair> BlockedCellPairs { get; }
        public List<Coordinates> AvailableWalls { get; }
        
        public event Action WallPlaced;

        public WallsManager()
        {
            Walls = new Wall[AmountOfRows, AmountOfColumns];
            
            BlockedCellPairs = new List<CellPair>();
            AvailableWalls = new List<Coordinates>();

            InitializeWallField();
        }
        
        public void PlaceWall(Player player, Coordinates wallCoordinates)
        {
            Wall wall = Walls[wallCoordinates.row, wallCoordinates.column];
            
            BlockedCellPairs.AddRange(wall.BlockedCellPairs);

            AvailableWalls.Remove(wallCoordinates);
            foreach (Coordinates overlappedWallCoordinates in wall.OverlappedWalls)
            {
                AvailableWalls.Remove(overlappedWallCoordinates);
            }
            
            player.PlaceWall();
            
            WallPlaced?.Invoke();
        }
        
        public void PlaceTemporaryWall(Coordinates wallCoordinates)
        {
            Wall wall = Walls[wallCoordinates.row, wallCoordinates.column];
            BlockedCellPairs.AddRange(wall.BlockedCellPairs);
        }
        public void DestroyTemporaryWall(Coordinates wallCoordinates)
        {
            Wall wall = Walls[wallCoordinates.row, wallCoordinates.column];
            foreach (CellPair cellPair in wall.BlockedCellPairs)
            {
                BlockedCellPairs.Remove(cellPair);
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
                            new[]
                            {
                                new CellPair(new Coordinates(i / 2, j), new Coordinates(i / 2, j + 1)),
                                new CellPair(new Coordinates(i / 2 + 1, j), new Coordinates(i / 2 + 1, j + 1))
                            },
                            new[]
                            {
                                new Coordinates(Math.Max(i - 2, 0), j),
                                new Coordinates(Math.Min(i + 2, Walls.GetLength(0) - 1), j),
                                new Coordinates(Math.Min(i + 1, Walls.GetLength(0) - 1), j)
                            });
                    }
                    else
                    {
                        Walls[i, j] = new Wall(
                            new[]
                            {
                                new CellPair(new Coordinates(i / 2, j), new Coordinates(i / 2 + 1, j)),
                                new CellPair(new Coordinates(i / 2, j + 1), new Coordinates(i / 2 + 1, j + 1))
                            },
                            new[]
                            {
                                new Coordinates(i, Math.Max(j - 1, 0)), new Coordinates(i, Math.Min(j + 1, Walls.GetLength(1) - 1)), new Coordinates(Math.Max(i - 1, 0), j)
                            });
                    }

                    AvailableWalls.Add(new Coordinates(i, j));
                }
            }
        }
    }
}

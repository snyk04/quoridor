using System;
using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.Model.Walls
{
    public class WallsManager
    { 
        public const int AmountOfRows = 16;
        public const int AmountOfColumns = 8;
        
        public Wall[,] Walls { get; }
        
        public List<CellPair> BlockedCellPairs { get; }
        // TODO : maybe rename?
        public List<Coordinates> WallsThatCanBePlaced { get; }
        
        public event Action WallPlaced;

        public WallsManager()
        {
            Walls = new Wall[AmountOfRows, AmountOfColumns];
            
            BlockedCellPairs = new List<CellPair>();
            WallsThatCanBePlaced = new List<Coordinates>();

            InitializeWallField();
        }
        
        // TODO : maybe rename?
        public void PathfindingPlaceWall(Coordinates wallCoordinates)
        {
            Wall wall = Walls[wallCoordinates.row, wallCoordinates.column];
            BlockedCellPairs.AddRange(wall.BlockedCellPairs);
        }
        public void PathfindingDestroyWall(Coordinates wallCoordinates)
        {
            Wall wall = Walls[wallCoordinates.row, wallCoordinates.column];
            foreach (CellPair cellPair in wall.BlockedCellPairs)
            {
                BlockedCellPairs.Remove(cellPair);
            }
        }
        
        // TODO : maybe move it to WallPlacer.cs?
        public void PlaceWall(Player player, Coordinates wallCoordinates)
        {
            Wall wall = Walls[wallCoordinates.row, wallCoordinates.column];
            
            BlockedCellPairs.AddRange(wall.BlockedCellPairs);

            WallsThatCanBePlaced.Remove(wallCoordinates);
            foreach (Coordinates overlappedWallCoordinates in wall.OverlappedWalls)
            {
                WallsThatCanBePlaced.Remove(overlappedWallCoordinates);
            }
            
            player.PlaceWall();
            
            WallPlaced?.Invoke();
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

                    WallsThatCanBePlaced.Add(new Coordinates(i, j));
                }
            }
        }
    }
}

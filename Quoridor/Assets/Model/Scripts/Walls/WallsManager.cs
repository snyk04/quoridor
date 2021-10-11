using System;
using System.Collections.Generic;
using Quoridor.Model.Cells;

namespace Quoridor.Model.Walls
{
    public class WallsManager
    { 
        public const int AmountOfRows = 16;
        public const int AmountOfColumns = 8;

        public Wall[,] Walls { get; }
        
        public List<CellPair> BlockedCellPairs { get; }
        private List<Coordinates> PlacedWalls { get; }
        public List<Coordinates> WallsThatCanBePlaced { get; }

        public WallsManager()
        {
            Walls = new Wall[AmountOfRows, AmountOfColumns];
            
            BlockedCellPairs = new List<CellPair>();
            PlacedWalls = new List<Coordinates>();
            WallsThatCanBePlaced = new List<Coordinates>();

            InitializeWallField();
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
                                new CellPair(new Coordinates(i / 2, j), new Coordinates(i / 2, j + 1)),
                                new CellPair(new Coordinates(i / 2 + 1, j), new Coordinates(i / 2 + 1, j + 1))
                            },
                            new List<Coordinates>
                            {
                                new Coordinates(Math.Max(i - 2, 0), j),
                                new Coordinates(Math.Min(i + 2, Walls.GetLength(0) - 1), j),
                                new Coordinates(Math.Min(i + 1, Walls.GetLength(0) - 1), j)
                            });
                    }
                    else
                    {
                        Walls[i, j] = new Wall(
                            new List<CellPair>
                            {
                                new CellPair(new Coordinates(i / 2, j), new Coordinates(i / 2 + 1, j)),
                                new CellPair(new Coordinates(i / 2, j + 1), new Coordinates(i / 2 + 1, j + 1))
                            },
                            new List<Coordinates>
                            {
                                new Coordinates(i, j - 1), new Coordinates(i, j + 1), new Coordinates(i - 1, j)
                            });
                    }

                    WallsThatCanBePlaced.Add(new Coordinates(i, j));
                }
            }
        }

        public bool WallCanBePlaced(Coordinates wallCoordinates)
        {
            return WallsThatCanBePlaced.Contains(wallCoordinates);
        }

        public void DestroyAllWalls()
        {
            foreach (Coordinates placedWall in PlacedWalls)
            {
                DestroyWall(placedWall);
            }
        }
        public void PlaceWall(Coordinates wallCoordinates, out List<Coordinates> overlappedWalls)
        {
            Wall wall = Walls[wallCoordinates.row, wallCoordinates.column];
            PlacedWalls.Add(wallCoordinates);
            
            overlappedWalls = wall.OverlappedWalls;

            BlockedCellPairs.AddRange(wall.BlockedCellPairs);

            WallsThatCanBePlaced.Remove(wallCoordinates);
            foreach (Coordinates overlappedWallCoordinates in wall.OverlappedWalls)
            {
                WallsThatCanBePlaced.Remove(overlappedWallCoordinates);
            }
        }
        public void DestroyWall(Coordinates wallCoordinates)
        {
            Wall wall = Walls[wallCoordinates.row, wallCoordinates.column];
            PlacedWalls.Remove(wallCoordinates);
            
            foreach (CellPair cellPair in wall.BlockedCellPairs)
            {
                BlockedCellPairs.Remove(cellPair);
            }

            WallsThatCanBePlaced.Add(wallCoordinates);
            foreach (Coordinates overlappedWallCoordinates in wall.OverlappedWalls)
            {
                WallsThatCanBePlaced.Add(overlappedWallCoordinates);
            }
        }
    }
}

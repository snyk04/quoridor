using System.Collections.Generic;
using Quoridor.Model.Cells;
using UnityEngine;

namespace Quoridor.View.Walls
{
    public class WallPlacer : MonoBehaviour
    {
        [SerializeField] private WallStorage _wallStorage;
        
        public void PlaceWall(Coordinates wallCoordinates, IEnumerable<Coordinates> overlappedWalls)
        {
            WallVisual wall = _wallStorage[wallCoordinates];
            wall.HandlePlace();

            foreach (Coordinates overlappedWall in overlappedWalls)
            {
                _wallStorage[overlappedWall].Disable();
            }
        }
    }
}

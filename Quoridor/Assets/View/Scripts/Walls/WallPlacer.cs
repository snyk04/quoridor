using Quoridor.Model.Cells;
using UnityEngine;

namespace Quoridor.View.Walls
{
    public class WallPlacer : MonoBehaviour
    {
        [SerializeField] private WallStorage _wallStorage;
        
        public void PlaceWall(CellCoordinates wallCoordinates)
        {
            _wallStorage.GetWall(wallCoordinates).Place();
        }
    }
}

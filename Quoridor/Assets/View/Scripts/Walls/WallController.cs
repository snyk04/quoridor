using System.Collections.Generic;
using System.Linq;
using Quoridor.Model.Common;
using Quoridor.View.Audio;
using UnityEngine;

namespace Quoridor.View.Walls
{
    public class WallController : MonoBehaviour
    {
        [SerializeField] private ViewCommunication _view;
        [SerializeField] private RandomSoundPlayer _wallSoundPlayer;

        private List<WallVisual> _placedWalls;

        private void Awake()
        {
            _placedWalls = new List<WallVisual>();
        }

        public void PlaceWall(Coordinates wallCoordinates)
        {
            WallVisual wall = _view.WallStorage[wallCoordinates];
            
            wall.HandlePlace();
            _placedWalls.Add(wall);

            _wallSoundPlayer.PlayNext();

            DisableAllWalls();
        }
        public void EnableWalls(IEnumerable<Coordinates> wallCoordinates)
        {
            foreach (Coordinates wallCoordinate in wallCoordinates)
            {
                WallVisual wall = _view.WallStorage[wallCoordinate];
                wall.Enable();
            }
        }

        private void DisableAllWalls()
        {
            foreach (WallVisual wall in _view.WallStorage.Except(_placedWalls))
            {
                wall.Disable();
            }
        }
    }
}

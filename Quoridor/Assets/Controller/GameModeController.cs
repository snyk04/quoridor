using Quoridor.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.Controller
{
    public class GameModeController : MonoBehaviour
    {
        [SerializeField] private Dropdown _gameModeDropdown;

        public GameMode GameMode => (GameMode)_gameModeDropdown.value;
    }
}

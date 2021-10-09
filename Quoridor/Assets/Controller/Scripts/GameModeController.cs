using Quoridor.Model.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.Controller
{
    public sealed class GameModeController : MonoBehaviour
    {
        [SerializeField] private Dropdown _gameModeDropdown;

        public GameMode GameMode => (GameMode)_gameModeDropdown.value;
    }
}

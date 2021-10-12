using Quoridor.Model.Game;
using UnityEngine;
using UnityEngine.UI;
using View.Scripts.UserInterface;

namespace Quoridor.Controller
{
    public sealed class GameModeController : MonoBehaviour
    {
        public GameMode GameMode => GameModeTransmitter.GameMode;
    }
}

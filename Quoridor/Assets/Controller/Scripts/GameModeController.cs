using Quoridor.NewModel.PlayerLogic;
using UnityEngine;
using View.Scripts.UserInterface;

namespace Quoridor.Controller
{
    public sealed class GameModeController : MonoBehaviour
    {
        public GameMode GameMode => GameModeTransmitter.GameMode;
    }
}

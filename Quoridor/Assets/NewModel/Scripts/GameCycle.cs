using System;
using Quoridor.NewModel.PlayerLogic;

namespace Quoridor.NewModel
{
    public class GameCycle
    {
        public event Action<GameMode> GameStarted;
        public event Action GameStopped;

        public void StartNewGame(GameMode gameMode)
        {
            GameStarted?.Invoke(gameMode);
        }
        public void StopGame()
        {
            GameStopped?.Invoke();
        }
    }
}

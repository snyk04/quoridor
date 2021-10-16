using System;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.Model
{
    public class GameCycle
    {
        private readonly ModelCommunication _model;
        
        public event Action<GameMode> GameStarted;
        public event Action GameStopped;

        public GameCycle(ModelCommunication model)
        {
            _model = model;
        }

        public void StartNewGame(GameMode gameMode)
        {
            GameStarted?.Invoke(gameMode);
        }
        public void StopGame()
        {
            _model.StopGame(GameStopType.Victory);
            GameStopped?.Invoke();
        }
    }
}

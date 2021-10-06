using System;
using Quoridor.Model.Players;

namespace Quoridor.Model.Game
{
    public class GameCycle
    {
        private readonly ModelCommunication _model;
        
        public GameMode GameMode { get; private set; }
        
        public event Action OnGameStart;
        public event Action OnGameEnd;

        public GameCycle(ModelCommunication model)
        {
            _model = model;
        }
        
        public void StartNewGame(GameMode gameMode)
        {
            GameMode = gameMode;
            
            OnGameStart?.Invoke();
        }
        public void EndGame(PlayerType winner)
        {
            OnGameEnd?.Invoke();
            _model.EndGame(winner);
        }
    }
}

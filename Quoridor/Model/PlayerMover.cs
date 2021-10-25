using Quoridor.Model.Cells;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.Model
{
    public sealed class PlayerMover
    {
        private readonly ModelCommunication _model;

        public PlayerMover(ModelCommunication model)
        {
            _model = model;
        }

        public void Move(Player player, Coordinates cellCoordinates)
        {
            Coordinates oldCellCoordinates = player.Position;

            Cell oldCell = _model.CellsManager[oldCellCoordinates];
            Cell newCell = _model.CellsManager[cellCoordinates];

            oldCell.BecomeFree();
            newCell.BecomeBusy();

            player.MoveTo(cellCoordinates);
            
            CheckPlayerVictory(player);
        }
        private void CheckPlayerVictory(Player player)
        {
            if (player.Position.row == player.VictoryRow)
            {
                _model.GameCycle.StopGame();
            }
        }
    }
}

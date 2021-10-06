namespace Quoridor.Model
{
    public interface IModel
    {
        void StartNewGame(GameMode gameMode);

        void MoveCurrentPlayerToCell(CellCoordinates cellCoordinates);
    }
}

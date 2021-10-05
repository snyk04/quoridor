namespace Quoridor.Model.New
{
    public interface IModel
    {
        void StartNewGame(GameMode gameMode);

        void MoveCurrentPlayerToCell(CellCoordinates cellCoordinates);
    }
}

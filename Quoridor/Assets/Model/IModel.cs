namespace Quoridor.Model
{
    public interface IModel
    {
        void StartGame();
        
        void MoveCurrentPawnToCell(CellCoordinates cellCoordinates);
    }
}

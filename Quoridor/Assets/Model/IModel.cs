namespace Quoridor.Model
{
    public interface IModel
    {
        void StartGame();
        
        void ShowAvailableCellsForCurrentPawn();
        void MovePawnToCell(PawnType pawnType, CellCoordinates cellCoordinates);
        void MoveCurrentPawnToCell(CellCoordinates cellCoordinates);
    }
}

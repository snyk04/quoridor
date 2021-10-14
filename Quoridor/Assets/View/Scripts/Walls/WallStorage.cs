using Quoridor.OldModel.Walls;

namespace Quoridor.View.Walls
{
    public class WallStorage : Storage<WallVisual>
    {
        protected override int AmountOfColumns => WallsManager.AmountOfColumns;
    }
}

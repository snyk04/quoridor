using Quoridor.Model.Walls;

namespace Quoridor.View.Walls
{
    public sealed class WallStorage : Storage<WallVisual>
    {
        protected override int AmountOfColumns => WallsManager.AmountOfColumns;
    }
}

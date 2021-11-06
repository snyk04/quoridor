namespace Quoridor.Controller.Buttons
{
    public sealed class WallButton : Button
    {
        public override void NotifyController()
        {
            _controller.PlaceWall(_coordinates);
        }
    }
}

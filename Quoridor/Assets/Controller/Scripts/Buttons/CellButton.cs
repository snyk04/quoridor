namespace Quoridor.Controller.Buttons
{
    public sealed class CellButton : Button
    {
        public override void NotifyController()
        {
            _controller.ChooseCell(_coordinates);
        }
    }
}

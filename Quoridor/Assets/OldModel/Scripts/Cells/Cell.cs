namespace Quoridor.OldModel.Cells
{
    public sealed class Cell
    {
        public bool IsBusy { get; private set; }

        public Cell()
        {
            BecomeFree();
        }

        public void BecomeBusy()
        {
            ChangeBusyness(true);
        }
        public void BecomeFree()
        {
            ChangeBusyness(false);
        }

        private void ChangeBusyness(bool isBusy)
        {
            IsBusy = isBusy;
        }
    }
}

namespace Quoridor.Model
{
    public class Cell
    {
        public bool IsBusy { get; private set; }

        public Cell()
        {
            IsBusy = false;
        }

        public void MakeBusy()
        {
            IsBusy = true;
        }
        public void MakeFree()
        {
            IsBusy = false;
        }
    }
}

namespace Quoridor.Model
{
    public class Cell
    {
        private bool _isBusy;

        public bool IsBusy => _isBusy;

        public Cell()
        {
            _isBusy = false;
        }

        public void MakeBusy()
        {
            _isBusy = true;
        }

        public void MakeFree()
        {
            _isBusy = false;
        }
    }
}
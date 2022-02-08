namespace Quoridor.Model.Common
{
    public sealed class Random : System.Random
    {
        public float Value => (float) Sample();
    }
}

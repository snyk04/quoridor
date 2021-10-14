using Quoridor.Model.Common;

namespace Quoridor.Model
{
    public class WallField
    {
        public Coordinates[] AvailableWalls()
        {
            // TODO
            Coordinates[] array = new Coordinates[128];
            
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    array[i * 8 + j] = new Coordinates(i, j);
                }
            }
            
            return array;
        }
    }
}

using System.Collections.Generic;

namespace GarageManager
{
    public class GarageManagerModel
    {
        public IGarageGrid Grid { get; private set; }

        public GarageManagerModel(IGarageGrid garageGrid)
        {
            Grid = garageGrid;
        }
    }
}

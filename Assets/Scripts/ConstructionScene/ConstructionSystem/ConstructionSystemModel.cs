using ItemSlotConstruction;
using OrderLine;
using System.Collections.Generic;

namespace ConstructionSystem
{
    public class ConstructionSystemModel
    {
        public List<ItemSlotConstructionPresenter> ItemSlotLines { get; private set; }

        public ConstructionSystemModel()
        {
            ItemSlotLines = new();
        }
    }
}

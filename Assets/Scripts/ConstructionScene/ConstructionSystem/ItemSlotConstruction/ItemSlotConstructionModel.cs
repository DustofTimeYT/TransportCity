namespace ItemSlotConstruction
{
    public class ItemSlotConstructionModel
    {
        public AbsTileConfig _config { get; private set; }

        public ItemSlotConstructionModel(AbsTileConfig config)
        {
            _config = config;
        }
    }
}

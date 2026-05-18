using ConstructionSystem;

namespace ItemSlotConstruction
{
    public class ItemSlotConstructionPresenter
    {
        private ItemSlotConstructionModel _model;
        private ConstructionSystemPresenter _constructionMenu;

        public ItemSlotConstructionPresenter(AbsTileConfig config, ConstructionSystemPresenter constructionMenu)
        {
            _model = new (config);
            _constructionMenu = constructionMenu;
        }

        public void CreateConstruction()
        {
            _constructionMenu.SetSelectedTileConfig(_model._config);
        }

        public string GetName()
        {
            return _model._config.Name;
        }
    }
}

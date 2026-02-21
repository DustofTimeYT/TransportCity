public class CargoItem : ICargoItem
{
    public ProductType ProductType { get; private set; }

    public CargoItem(ProductType productType)
    {
        ProductType = productType;
    }

    public ProductType GetProductType()
    {
        return ProductType;
    }
}
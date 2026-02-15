using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IConsumer
{
    public Vector2Int GetTilePosition();

    public string GetName();

    public List<ProductType> GetConsumerMaterials();

    public bool CheckConsumerMaterial(ProductType product);
}

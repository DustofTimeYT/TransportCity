using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Product
{
    [field: SerializeField]
    public ProductType Type { get; set; }

    [field: SerializeField]
    public float CycleTime { get; set; }

    [field: SerializeField]
    public float CycleOutput { get; set; }
}

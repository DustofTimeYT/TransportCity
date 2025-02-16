using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSceneService : MonoBehaviour
{
    public GridSystem _gridSystem;

    private void Awake()
    {
        _gridSystem.GridGenerator();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

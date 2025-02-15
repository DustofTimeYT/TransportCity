using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionGridScr : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);

    private ConstructionScr[,] _grid;
    private ConstructionScr _flyingConstruction;
    private Camera _mainCamera;

    private void Awake()
    {
        _grid = new ConstructionScr[gridSize.x*2, gridSize.y*2];

        _mainCamera = Camera.main;
    }

    public void StartPlacingConstruction(ConstructionScr constructionPrefab)
    {
        if (_flyingConstruction != null)
        {
            Destroy(_flyingConstruction.gameObject);
        }

        _flyingConstruction = Instantiate(constructionPrefab);
    }

    private void Update()
    {
        if (_flyingConstruction != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if(groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                var x = Mathf.RoundToInt(worldPosition.x);
                var y = Mathf.RoundToInt(worldPosition.z);

                _flyingConstruction.transform.position = new Vector3(x,0,y);

                bool _available = true;

                if (Mathf.Abs(x) > gridSize.x - _flyingConstruction.size.x)
                {
                    _available = false;
                }
                if (Mathf.Abs(y) > gridSize.y - _flyingConstruction.size.y)
                {
                    _available = false;
                }

                if (_available && IsPlaceTaken(x,y))
                {
                    _available = false ;
                }

                _flyingConstruction.SetTransparent(_available);

                if (_available && Input.GetMouseButtonDown(0))
                {
                    PlacingFlyingConstruction(x,y);
                }

            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for(int x = 0; x < _flyingConstruction.size.x; x++)
        {
            for (int y = 0; y < _flyingConstruction.size.y; y++)
            {
                if (_grid[gridSize.x + placeX + x, gridSize.y + placeY + y] != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void PlacingFlyingConstruction(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingConstruction.size.x; x++)
        {
            for (int y = 0; y < _flyingConstruction.size.y; y++)
            {
                _grid[gridSize.x + placeX + x, gridSize.y + placeY + y] = _flyingConstruction;
            }
        }

        _flyingConstruction = null;
    }
}

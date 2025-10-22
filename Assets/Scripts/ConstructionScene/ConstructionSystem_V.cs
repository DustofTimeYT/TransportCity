using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSystem_V : MonoBehaviour
{
    private ConstructionSystem_P _cS_P;
    [SerializeField] private CellsConfig _cellsConfig;
    private GameObject _flyingCell;
    private CellConfig _currentCellConfig;
    private Camera _mainCamera;

    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _buttonPrefab;

    private void Awake()
    {
        _mainCamera = Camera.main;
        CreateUIMenu();
    }

    public void Bind(ConstructionSystem_P cS_P)
    {
        _cS_P = cS_P;
    }

    private void CreateUIMenu()
    {
        CounstructionCellItem CCI;
        foreach (var item in _cellsConfig.Cells)
        {
            CCI = Instantiate(_buttonPrefab, _container).GetComponent<CounstructionCellItem>();
            CCI.Init(this,item);
        }
    }

    public void StartPlacingConstruction(CellConfig cellConfig)
    {
        if (_flyingCell != null)
        {
            _currentCellConfig = null;
            Destroy(_flyingCell);
        }

        _currentCellConfig = cellConfig;
        _flyingCell = Instantiate(_currentCellConfig.CellPref);
    }

    public void CreateFlyCell()
    {

    }

    private void Update()
    {
        if (_flyingCell != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if(groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                var x = Mathf.RoundToInt(worldPosition.x);
                var y = Mathf.RoundToInt(worldPosition.z);

                _flyingCell.transform.position = new Vector3(x,0,y);

                bool _available = true;
/*
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

*/
                if (/*_available && */Input.GetMouseButtonDown(0))
                {
                    PlacingFlyingConstruction(x,y);
                }

            }
        }
    }

    private void PlacingFlyingConstruction(int placeX, int placeY)
    {
        if(_cS_P.TryPlaceBuilding(new Vector2Int (placeX, placeY), _currentCellConfig, out AbstractCellPresenter cell))
        {
            /*AbstractCellView cellView = Instantiate(_currentCellConfig.CellPref).GetComponent<AbstractCellView>();
            Debug.Log(cellView);*/

            //_flyingCell = null;
        }
    }
}

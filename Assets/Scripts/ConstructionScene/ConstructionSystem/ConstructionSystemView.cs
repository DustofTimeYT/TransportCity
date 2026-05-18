using AbsMenu;
using ConstructionSystem;
using ItemSlotConstruction;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ConstructionSystemView : AbsMenuView<ConstructionSystemPresenter, ItemSlotConstructionPresenter, ItemSlotConstructionView>
{
    private const int _rotationAngle = 90;

    [Inject]
    private ConstructionSystemPresenter _cS_P;

    private GameObject _flyingTile;
    private AbsTileConfig _currentTileConfig;
    private Camera _mainCamera;

    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _buttonPrefab;

    private Plane groundPlane;
    private Ray ray;

    protected override void Awake()
    {
        base.Awake();

        _mainCamera = Camera.main;
        //CreateUIMenu();
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        PlacecingTile();
        
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        _presenter.TileConfigSelected += StartPlacingTile;
    }

    private void PlacecingTile()
    {
        Cancel();

        if (_flyingTile != null)
        {
            ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                Vector3Int newPos = new Vector3Int(Mathf.RoundToInt(worldPosition.x), 0, Mathf.RoundToInt(worldPosition.z));

                if (newPos != _flyingTile.transform.position)
                {
                    _cS_P.ShowTile(_flyingTile.transform.position);

                    _flyingTile.transform.position = newPos;

                    _cS_P.HideTile(newPos);
                }

                RotateTile();

                if (Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingTile(newPos.x, newPos.z);
                }

            }
        }
    }

    private void Cancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_flyingTile != null)
            {
                _cS_P.ShowTile(_flyingTile.transform.position);
                Destroy(_flyingTile);
                _currentTileConfig = null;
                _flyingTile = null;
            }
        }
    }

    private void RotateTile()
    {
        if (Input.GetKeyDown(KeyCode.Q)) SetRotation(_rotationAngle);

        if (Input.GetKeyDown(KeyCode.E)) SetRotation(-_rotationAngle);
    }

    private void SetRotation(int rotationAngle)
    {
        _flyingTile.transform.Rotate(0, rotationAngle, 0);
    }

    public void StartPlacingTile(AbsTileConfig config)
    {
        if (_flyingTile != null)
        {
            _currentTileConfig = null;
            Destroy(_flyingTile);
        }

        _currentTileConfig = config;
        _flyingTile = Instantiate(_currentTileConfig.TilePref);
    }

    private void PlaceFlyingTile(int placeX, int placeY)
    {
        _cS_P.TryPlaceTile(new Vector2Int(placeX, placeY), (int)_flyingTile.transform.rotation.eulerAngles.y, _currentTileConfig);
    }
}

using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ConstructionSystem_V : MonoBehaviour
{
    [Inject]
    private ConstructionSystem_P _cS_P;
    [Inject]
    private TilesConfig _tilesConfig;

    private GameObject _flyingTile;
    private AbsTileConfig _currentTileConfig;
    private Camera _mainCamera;

    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _buttonPrefab;

    private Plane groundPlane;
    private Ray ray;

    private void Awake()
    {
        _mainCamera = Camera.main;
        CreateUIMenu();
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
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


                if (Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingTile(newPos.x, newPos.z);
                }

            }
        }
    }

    private void Cancel()
    {
        if (Input.GetKey(KeyCode.Escape))
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

    private void CreateUIMenu()
    {
        ItemSlotConstruction CCI;
        foreach (var item in _tilesConfig.GetAllTiles())
        {
            GameObject slot = Instantiate(_buttonPrefab);
            slot.transform.SetParent(_container);
            //Instantiate(item.TilePref, slot.transform);
            CCI = slot.GetComponent<ItemSlotConstruction>();
            CCI.Init(this,item);
        }
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
        _cS_P.TryPlaceTile(new Vector2Int(placeX, placeY), _currentTileConfig);
    }
}

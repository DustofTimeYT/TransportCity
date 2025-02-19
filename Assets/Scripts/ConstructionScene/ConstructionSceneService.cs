using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ConstructionSceneService : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private CellConfig _cellConfig;
    [SerializeField] private CellView _cellView;

    private GridModel _gridModel;
    private GridPresenter _gridPresenter;

    public PathFinding _pathFindingSystem;

    public ConstructionSceneUI _constructionSceneUI;

    private Dictionary<Vector2Int, CellPresenter> _grid;

    private void Awake()
    {

        UIEventBus UIEventBus = new UIEventBus();
        _constructionSceneUI.Init(UIEventBus);
        _grid = GridGenerator.GenerateGridModel(_gridConfig, _cellConfig, _cellView, UIEventBus);
        _gridModel = new GridModel(_grid);
        _gridPresenter = new GridPresenter(UIEventBus, _gridModel);
        _pathFindingSystem.Init(_grid);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var startCell = _gridModel.StartCell.GetCellPosition();
            var endCell = _gridModel.EndCell.GetCellPosition();
            if(_pathFindingSystem.TryPathFind(startCell, endCell, out IReadOnlyList<Vector2Int> path))
            {
                StartCoroutine(_gridPresenter.DisplayPath(path)); 
            }
        }
    }
}

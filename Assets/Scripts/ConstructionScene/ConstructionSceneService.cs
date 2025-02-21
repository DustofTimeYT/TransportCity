using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Точка входа в приложение (в будущем перенести на Zenject) 
/// </summary>

public class ConstructionSceneService : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private CellConfig _cellConfig;
    [SerializeField] private CellView _cellView;

    private GridModel _gridModel;
    private GridPresenter _gridPresenter;

    private PathFinding _pathFindingSystem;

    public ConstructionSceneUI _constructionSceneUI;

    private Dictionary<Vector2Int, CellPresenter> _grid;

    private void Awake()
    {

        UIEventBus UIEventBus = new UIEventBus();
        _constructionSceneUI.Init(UIEventBus);
        _grid = GridGenerator.GenerateGridModel(_gridConfig, _cellConfig, _cellView, UIEventBus);
        _gridModel = new GridModel(_grid);
        _gridPresenter = new GridPresenter(UIEventBus, _gridModel);
        _pathFindingSystem = new PathFinding();
        SurroundingCellsGenerator surroundingCellsGenerator = new(_gridConfig);
        _pathFindingSystem.Init(surroundingCellsGenerator, _grid);
    }

    void Update()
    {
        CalculatePath();
    }

    /// <summary>
    /// Метод вызова рассчета пути и его отображения
    /// </summary>

    private void CalculatePath()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var startCell = _gridModel.StartCell.GetCellPosition();
            var endCell = _gridModel.EndCell.GetCellPosition();
            if (_pathFindingSystem.TryPathFind(startCell, endCell, out IReadOnlyList<Vector2Int> path))
            {
                StartCoroutine(_gridPresenter.DisplayPath(path));
            }
        }
    }
}

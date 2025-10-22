using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Точка входа в приложение (в будущем перенести на Zenject) 
/// </summary>

public class ConstructionSceneService : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private CellConfig _cellConfig;
    [SerializeField] private RoadCellView _cellView;

    private GridModel _gridModel;
    private GridPresenter _gridPresenter;

    private PathFinding _pathFindingSystem;

    public ConstructionSceneUI _constructionSceneUI;

    public ConstructionSystem_V ConstructionSystem_V;


    private void Awake()
    {

        UIEventBus UIEventBus = new UIEventBus();
        _constructionSceneUI.Init(UIEventBus);
        Dictionary<Vector2Int, AbstractCellPresenter> _grid = GridGenerator.GenerateGridModel(_gridConfig, _cellConfig, _cellView, UIEventBus);
        _gridModel = new GridModel(_grid);
        _gridPresenter = new GridPresenter(UIEventBus, _gridModel);
        SurroundingCellsFinder surroundingCellsFinder = new();
        _pathFindingSystem = new PathFinding(surroundingCellsFinder, _gridPresenter);

        ConstructionSystem_V.Bind(new ConstructionSystem_P(_gridPresenter));
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
            Vector2Int startCell = new(1, 0);
            Vector2Int endCell = new(9, 0);
            if (_pathFindingSystem.TryPathFind(startCell, endCell, out IReadOnlyList<Vector2Int> path))
            {
                Debug.Log(path.Count);
                if (path != null) foreach (var cell in path) { Debug.Log(cell); }
                //StartCoroutine(_gridPresenter.DisplayPath(path));
            }
        }
    }
}

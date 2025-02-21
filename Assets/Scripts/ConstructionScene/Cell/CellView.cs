using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Представление клетки
/// </summary>

public class CellView : MonoBehaviour
{
    private CellPresenter _presenter;
    [SerializeField] private Renderer _mainRenderer;
    [SerializeField] private Material _defaulteColor;
    [SerializeField] private Material _startColor;
    [SerializeField] private Material _endColor;
    [SerializeField] private Material _unrichmentColor;
    [SerializeField] private Material _highlightColor;
    [SerializeField] private TextMeshProUGUI _textMovementDifficulty;
    public void Init(CellPresenter presenter, Vector2Int cellCoordinates)
    {
        _presenter = presenter;
        gameObject.transform.position = new Vector3(cellCoordinates.x, 0 , cellCoordinates.y);

        _presenter.UpdateView();
    }

    /// <summary>
    /// Получение доступных действий с клеткой
    /// </summary>
    /// <returns>список доступных действий с клеткой</returns>

    public List<CellActionData> GetActionData()
    {
        return _presenter.GetActionData();
    }

    /// <summary>
    /// Изменение цвета клетки в соответствии с ее типом
    /// </summary>
    /// <param name="cellStateType">Тип клетки</param>

    public void SetColor(CellStateType cellStateType)
    {
        switch (cellStateType)
        {
            case CellStateType.StartCell:
                _mainRenderer.material = _startColor;
                _textMovementDifficulty.gameObject.SetActive(false);
                break;

            case CellStateType.EndCell:
                _mainRenderer.material = _endColor;
                _textMovementDifficulty.gameObject.SetActive(false);
                break;

            case CellStateType.AvailableCell:
                _mainRenderer.material = _defaulteColor;
                _textMovementDifficulty.gameObject.SetActive(true);
                break;

            case CellStateType.UnrichmentCell:
                _mainRenderer.material = _unrichmentColor;
                _textMovementDifficulty.gameObject.SetActive(false);
                break;

            default:
                Debug.LogError("No create method for type " + cellStateType);
                break;
        }
    }

    /// <summary>
    /// Метод переключения подсветки клетки
    /// </summary>
    /// <param name="isSelect">Показатель выбрана ли клетка</param>

    public void HighLightCell(bool isSelect)
    {
        if (isSelect)
        {
            _mainRenderer.material = _highlightColor;
            _textMovementDifficulty.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Метод перерисовки данных
    /// </summary>
    /// <param name="cellModel">Данные клетки</param>

    public void UpdateView(CellModel cellModel)
    {
        _textMovementDifficulty.text = cellModel.MovementDifficulty.ToString();
        SetColor(cellModel.CellStateType);
        HighLightCell(cellModel.IsInPath);
    }
}

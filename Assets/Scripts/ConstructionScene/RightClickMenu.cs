using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _root;
    [SerializeField] private Button _setStartPosButton;
    [SerializeField] private Button _setEndPosButton;
    [SerializeField] private Button _setAvailabelCellButton;
    [SerializeField] private Button _setUnrichmentCellButton;

    private float _rectHeight;
    private float _rectWidht;

    private UIEventBus _UIEventBus;

    public void Init(Transform canvas, UIEventBus UIEventBus)
    {
        _UIEventBus = UIEventBus;
        _rectHeight = _root.rect.height;
        _rectWidht = _root.rect.width;
        Hide();
    }

    public void ShowRightClickedMenu(List<CellActionData> cellActions)
    {
        Hide();
        SetRCMPosition();
        _root.gameObject.SetActive(true);

        foreach (var actionData in cellActions)
        {
            var button = GetButtonByType(actionData.CellActionType);
            button.gameObject.SetActive(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(actionData.Callback);
            button.onClick.AddListener(Hide);
        }
    }

    private void SetRCMPosition()
    {
        gameObject.transform.position = new Vector3(Input.mousePosition.x + _rectWidht / 2,
            Input.mousePosition.y - _rectHeight / 2, Input.mousePosition.z);
    }

    public void Hide()
    {
        _root.gameObject.SetActive(false);
        _setStartPosButton.gameObject.SetActive(false);
        _setEndPosButton.gameObject.SetActive(false);
        _setAvailabelCellButton.gameObject.SetActive(false);
        _setUnrichmentCellButton.gameObject.SetActive(false);
    }

    private Button GetButtonByType(CellActionType cellActionType)
    {
        switch (cellActionType)
        {
            case CellActionType.Start:
                return _setStartPosButton;

            case CellActionType.End:
                return _setEndPosButton;

            case CellActionType.Availabel:
                return _setAvailabelCellButton;

            case CellActionType.Disable:
                return _setUnrichmentCellButton;

            default:
                Debug.LogError("Not all enums for TowerActionPopup covered! Unknown type " + cellActionType);
                return null;
        }
    }
}

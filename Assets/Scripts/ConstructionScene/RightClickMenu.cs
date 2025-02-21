using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _root;
    [SerializeField] private TMP_InputField _setMovementDifficultyInputField;
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
            switch (actionData.UITypeAction) 
            {
                case UITypeAction.Button:
                    var button = GetButtonByType(actionData.CellActionType);
                    button.gameObject.SetActive(true);
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(actionData.Callback);
                    button.onClick.AddListener(Hide);
                    break;

                case UITypeAction.InputField:
                    var inputField = GetInputFieldType(actionData.CellActionType);
                    inputField.gameObject.SetActive(true);
                    inputField.onSubmit.RemoveAllListeners();
                    inputField.onSubmit.AddListener(actionData.CallbackString);
                    inputField.ActivateInputField();
                    break;
            }
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
        _setMovementDifficultyInputField.gameObject.SetActive(false);
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
                Debug.LogError("Not all enums for RightClickMenu covered! Unknown type " + cellActionType);
                return null;
        }
    }

    private TMP_InputField GetInputFieldType(CellActionType cellActionType)
    {
        switch (cellActionType)
        {
            case CellActionType.MovementDifficulty:
                return _setMovementDifficultyInputField;

            default:
                Debug.LogError("Not all enums for RightClickMenu covered! Unknown type " + cellActionType);
                return null;
        }
    }
}

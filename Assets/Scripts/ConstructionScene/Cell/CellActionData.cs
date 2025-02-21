using UnityEngine.Events;

/// <summary>
/// Класс передающий действие с клеткой
/// </summary>

public class CellActionData
{
    public readonly UITypeAction UITypeAction;
    public readonly CellActionType CellActionType;
    public readonly UnityAction Callback;
    public readonly UnityAction<string> CallbackString;

    public CellActionData(UITypeAction uiTypeAction, CellActionType cellActionType, UnityAction callback)
    {
        UITypeAction = uiTypeAction;
        CellActionType = cellActionType;
        Callback = callback;
    }

    public CellActionData(UITypeAction uiTypeAction, CellActionType cellActionType, UnityAction<string> callback)
    {
        UITypeAction = uiTypeAction;
        CellActionType = cellActionType;
        CallbackString = callback;
    }
}

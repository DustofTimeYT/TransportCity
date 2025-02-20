using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CellActionData
{
    public readonly UITypeAction UITypeAction;
    public readonly CellActionType CellActionType;
    public readonly UnityAction Callback;
    public readonly UnityAction<string> CallbackInt;

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
        CallbackInt = callback;
    }
}

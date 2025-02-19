using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CellActionData
{
    public readonly CellActionType CellActionType;
    public readonly UnityAction Callback;

    public CellActionData(CellActionType cellActionType, UnityAction callback)
    {
        CellActionType = cellActionType;
        Callback = callback;
    }
}

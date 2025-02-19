using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventBus : IReadOnlyUIEventBus
{
    public event Action<CellPresenter> SetStartCell;

    public event Action<CellPresenter> SetEndCell;

    public void TriggerSetStartCell(CellPresenter startCell)
    {
        SetStartCell?.Invoke(startCell); 
    }

    public void TriggerSetEndCell(CellPresenter endCell)
    {
        SetEndCell?.Invoke(endCell);
    }
}

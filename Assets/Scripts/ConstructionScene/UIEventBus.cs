using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventBus : IReadOnlyUIEventBus
{
    public event Action<RoadCellPresenter> SetStartCell;

    public event Action<RoadCellPresenter> SetEndCell;

    public void TriggerSetStartCell(RoadCellPresenter startCell)
    {
        SetStartCell?.Invoke(startCell); 
    }

    public void TriggerSetEndCell(RoadCellPresenter endCell)
    {
        SetEndCell?.Invoke(endCell);
    }
}

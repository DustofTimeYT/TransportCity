using System;
using UnityEngine;
using UnityEngine.Events;

public interface IReadOnlyUIEventBus
{
    public event Action<RoadCellPresenter> SetStartCell;

    public event Action<RoadCellPresenter> SetEndCell;
}

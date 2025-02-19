using System;
using UnityEngine;
using UnityEngine.Events;

public interface IReadOnlyUIEventBus
{
    public event Action<CellPresenter> SetStartCell;

    public event Action<CellPresenter> SetEndCell;
}

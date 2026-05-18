using System;
using System.Collections.Generic;

public interface IMenuPresenter<LinePresenter>
{
    public List<LinePresenter> GetLines();

    public event Action UpdateView;
}
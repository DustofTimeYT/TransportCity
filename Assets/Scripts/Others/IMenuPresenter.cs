using System;
using System.Collections.Generic;

public interface IMenuPresenter<T>
{
    public List<T> GetLines();

    public event Action UpdateView;
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Transport;
using UnityEngine;

public class TransportView : MonoBehaviour
{
    private TransportPresenter _presenter;

    public void Bind(TransportPresenter presenter)
    {
        _presenter = presenter;
        Hide();
        SetPosition(_presenter.GetPosition());
        Subscribe();
    }

    private void Subscribe()
    {
        _presenter.StartRoute += OnStartRoute;
        _presenter.StartReturnToBase += OnStartReturnToBase;
    }

    private void OnStartReturnToBase()
    {
        Show();
        StartCoroutine(StartRoute(_presenter.FindPathToBase()));
    }

    private void OnStartRoute()
    {
        Show();
        StartCoroutine(NextEndpoint());
    }

    public IEnumerator NextEndpoint()
    {
        yield return StartCoroutine(StartRoute(_presenter.FindPathToLoad()));

        yield return new WaitForSeconds(3);
        _presenter.LoadCargo();
        yield return StartCoroutine(StartRoute(_presenter.FindPathToUnload()));

        yield return new WaitForSeconds(3);
        _presenter.UnloadCargo();

        yield break;
    }

    public IEnumerator StartRoute(IReadOnlyList<Vector2Int> path)
    {
        Vector2Int currentPos = _presenter.GetPosition();

        foreach (Vector2Int tilePos in path)
        {
            SetPosition(tilePos);
            yield return new WaitForSeconds(1);
        }
        yield break;
    }

    private Vector2Int GetPosition()
    {
        var pos = transform.position;
        return new((int) pos.x, (int)pos.z);
    }
    private void SetPosition(Vector2Int pos)
    {
        transform.position = new(pos.x, 0, pos.y);
        _presenter.SetPosition(transform.position);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

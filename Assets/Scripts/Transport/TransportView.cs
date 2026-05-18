using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Transport;
using UnityEngine;

public class TransportView : MonoBehaviour
{
    private const int _rotationAngle = 45;
    private float _waitRouteTime;
    private float _waitLoadTime;

    private TransportPresenter _presenter;

    private Coroutine _routeCoroutine;

    public void Bind(TransportPresenter presenter)
    {
        _presenter = presenter;
        if (_presenter.GetMaxSpeed() != 0)
            _waitRouteTime = 100f / _presenter.GetMaxSpeed();
        else
            _waitRouteTime = 2f;

        _waitLoadTime = 0.2f * _presenter.GetMaxCapacity();

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
        _routeCoroutine = StartCoroutine(StartRoute(_presenter.FindPathToBase()));
    }

    private void OnStartRoute()
    {
        Show();
        _routeCoroutine = StartCoroutine(NextEndpoint());
    }

    public IEnumerator NextEndpoint()
    {
        yield return StartCoroutine(StartRoute(_presenter.FindPathToLoad()));

        yield return new WaitForSeconds(_waitLoadTime);
        _presenter.LoadCargo();
        yield return StartCoroutine(StartRoute(_presenter.FindPathToUnload()));

        yield return new WaitForSeconds(_waitLoadTime);
        _presenter.UnloadCargo();

        yield break;
    }

    public IEnumerator StartRoute(IReadOnlyList<Vector2Int> path)
    {
        Vector2Int prevPos = _presenter.GetPosition();

        if (path == null)
        {
            Debug.Log("Нет пути для движения. Маршрут остановлен");
            StopCoroutine(_routeCoroutine);
            yield break;
        }

        gameObject.transform.LookAt(new Vector3(path[0].x, 0, path[0].y));

        foreach (Vector2Int nextPos in path)
        {
            Vector2Int curPos = _presenter.GetPosition();

            if ((prevPos.x == curPos.x && curPos.x == nextPos.x) || (prevPos.y == curPos.y && curPos.y == nextPos.y))
            {
                gameObject.transform.LookAt(new Vector3(nextPos.x, 0, nextPos.y));
                yield return new WaitForSeconds(_waitRouteTime);
            }
            else
            {
                if (curPos.x - prevPos.x > 0 && curPos.y == prevPos.y)
                {
                    if (nextPos.y - curPos.y > 0)
                    {
                        SetRotation(-_rotationAngle);
                        yield return new WaitForSeconds(_waitRouteTime);
                        SetRotation(-_rotationAngle);
                    }
                    else if (nextPos.y - curPos.y < 0)
                    {
                        SetRotation(_rotationAngle);
                        yield return new WaitForSeconds(_waitRouteTime);
                        SetRotation(_rotationAngle);
                    }
                }

                if (curPos.x - prevPos.x < 0 && curPos.y == prevPos.y)
                {
                    if (nextPos.y - curPos.y > 0)
                    {
                        SetRotation(_rotationAngle);
                        yield return new WaitForSeconds(_waitRouteTime);
                        SetRotation(_rotationAngle);
                    }
                    else if (nextPos.y - curPos.y < 0)
                    {
                        SetRotation(-_rotationAngle);
                        yield return new WaitForSeconds(_waitRouteTime);
                        SetRotation(-_rotationAngle);
                    }
                }


                if (curPos.y - prevPos.y > 0 && curPos.x == prevPos.x)
                {
                    if (nextPos.x - curPos.x > 0)
                    {
                        SetRotation(_rotationAngle);
                        yield return new WaitForSeconds(_waitRouteTime);
                        SetRotation(_rotationAngle);
                    }
                    else if (nextPos.x - curPos.x < 0)
                    {
                        SetRotation(-_rotationAngle);
                        yield return new WaitForSeconds(_waitRouteTime);
                        SetRotation(-_rotationAngle);
                    }
                }

                if (curPos.y - prevPos.y < 0 && curPos.x == prevPos.x)
                {
                    if (nextPos.x - curPos.x > 0)
                    {
                        SetRotation(-_rotationAngle);
                        yield return new WaitForSeconds(_waitRouteTime);
                        SetRotation(-_rotationAngle);
                    }
                    else if (nextPos.x - curPos.x < 0)
                    {
                        SetRotation(_rotationAngle);
                        yield return new WaitForSeconds(_waitRouteTime);
                        SetRotation(_rotationAngle);
                    }
                }
            }

            SetPosition(nextPos);;
            prevPos = curPos;
        }
        yield break;
    }

    private void LookAtTile(Vector2Int nextPos, Vector2Int prevPos)
    {
        Vector2Int curPos = _presenter.GetPosition();
        if ((prevPos.x == curPos.x && curPos.x == nextPos.x) || (prevPos.y == curPos.y && curPos.y == nextPos.y))
        {
            gameObject.transform.LookAt(new Vector3(nextPos.x, 0, nextPos.y));
        }
        else
        {
            SetRotation(_rotationAngle);
        }

    }

    private void SetRotation(int rotationAngle)
    {
        gameObject.transform.Rotate(0, rotationAngle, 0);
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

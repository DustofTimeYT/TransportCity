using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UIElements;

public class ConstructionSceneUI : MonoBehaviour
{
    public Transform canvas;
    public Camera mainCamera;
    private RightClickMenu _rightClickMenu;
    public GameObject rightClickMenuPrefab;

    private UIEventBus _UIEventBus;

    public void Init(UIEventBus UIEventBus)
    {
        _UIEventBus = UIEventBus;
        _rightClickMenu = Instantiate(rightClickMenuPrefab, canvas).GetComponent<RightClickMenu>();
        _rightClickMenu.Init(canvas, _UIEventBus);
    }

    public Vector2Int GetCursorPosition()
    {
        var _position = new Vector2Int();

        var groundPlane = new Plane(Vector3.up, Vector3.zero);

        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 worldPosition = ray.GetPoint(position);

            _position.x = Mathf.RoundToInt(worldPosition.x);
            _position.y = Mathf.RoundToInt(worldPosition.z);

        }
        return _position;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                CellView cell = hit.collider.GetComponentInParent<CellView>();
                if (cell != null)
                {
                    if (cell != null)
                    {
                        var data = cell.GetActionData();
                        _rightClickMenu.ShowRightClickedMenu(data);
                    }
                }
                else
                {
                    _rightClickMenu.Hide();
                }
            }
            else
            {
                _rightClickMenu.Hide();
            }
        }
    }

}

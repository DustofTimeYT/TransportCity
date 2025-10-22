using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControlScr : MonoBehaviour
{
    [SerializeField] private PreferencesConfig _PrefConfig;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform cameraPivotTranslate;
    [SerializeField] private Transform cameraPivotRotate;
    [SerializeField] private Transform cameraPivotZoom;

    private float _moveSpeed; // скорость перемещения пивота камеры
    private float _rotateSpeed; // скорость вращения пивота камеры
    private float _zoomSpeed; // скорость зумирования пивота камеры

    [SerializeField] private int _maxZoom; // максимальное расстояние отдаления камеры
    [SerializeField] private int _minZoom; // минимальное расстояние отдаления камеры

    [Range(5f, 80f)]
    [SerializeField] private int _maxAngleAxisX; // максимальный угол наклона камеры
    [Range(5f, 80f)]
    [SerializeField] private int _minAngleAxisX; // минимальный угол наклона камеры

    [Range(3f, 15f)]
    [SerializeField] private float _cameraDelay; // задержка перемещения камеры к ее пивоту

    private void Awake()
    {
        cameraPivotRotate.rotation = Quaternion.Euler(new Vector3(_minAngleAxisX, 0, 0));
        _moveSpeed = (float)(_PrefConfig.MCSpeed * 0.01);
        _rotateSpeed = (float)(_PrefConfig.RCSpeed * 0.1);
        _zoomSpeed = (float)(_PrefConfig.RCSpeed * 0.01);
        cameraPivotZoom.LookAt(cameraPivotTranslate);

    }

    private void Update()
    {

        Movement();
        Zoom();
        Rotation();

        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, cameraPivotZoom.rotation, Time.deltaTime * _cameraDelay);
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, cameraPivotZoom.position, Time.deltaTime * _cameraDelay);

    }

    /// <summary>
    /// Получение изменения позиции курсора мыши
    /// </summary>
    /// <returns> смещение курсора мыши по двум осям</returns>
    private Vector2 GetMouseDeltaPos()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    /// <summary>
    /// Метод управления перемещением камеры
    /// </summary>
    private void Movement()
    {
        if (Input.GetKey(_PrefConfig.MCDrag))
        {
            cameraPivotTranslate.Translate(new Vector3(GetMouseDeltaPos().x * _PrefConfig.MCSpeed, 0, GetMouseDeltaPos().y * _PrefConfig.MCSpeed));
        }

        if (Input.GetKey(_PrefConfig.MCFoward))
        {
            cameraPivotTranslate.Translate(new Vector3(0f, 0f, _moveSpeed));
        }
        if (Input.GetKey(_PrefConfig.MCBackward))
        {
            cameraPivotTranslate.Translate(new Vector3(0f, 0f, -_moveSpeed));
        }
        if (Input.GetKey(_PrefConfig.MCLeft))
        {
            cameraPivotTranslate.Translate(new Vector3(-_moveSpeed, 0f, 0f));
        }
        if (Input.GetKey(_PrefConfig.MCRight))
        {
            cameraPivotTranslate.Translate(new Vector3(_moveSpeed, 0f, 0f));
        }
    }

    /// <summary>
    /// Метод управления приближением камеры
    /// </summary>
    private void Zoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            var zoom = cameraPivotZoom.localPosition.y + Input.mouseScrollDelta.y;
            if (zoom >= _minZoom && zoom <= _maxZoom)
            {
                SetZoom(zoom);
            }
        }
        if (Input.GetKey(_PrefConfig.ZCIn) || Input.GetKey(_PrefConfig.ZCOut)) 
        {
            if (Input.GetKey(_PrefConfig.ZCIn) && cameraPivotZoom.localPosition.y - _zoomSpeed > _minZoom)
            {
                SetZoom(cameraPivotZoom.localPosition.y - _zoomSpeed);
            }
            if (Input.GetKey(_PrefConfig.ZCOut) && cameraPivotZoom.localPosition.y + _zoomSpeed < _maxZoom)
            {
                SetZoom(cameraPivotZoom.localPosition.y + _zoomSpeed);
            }
        }
    }

    private void SetZoom(float zoom)
    {
        cameraPivotZoom.SetLocalPositionAndRotation(new Vector3(cameraPivotZoom.localPosition.x, zoom, cameraPivotZoom.localPosition.z), cameraPivotZoom.localRotation);
    }

    /// <summary>
    /// Метод управления вращением камеры
    /// </summary>
    private void Rotation()
    {
        if (Input.GetKey(_PrefConfig.RCDrag))
        {
            var eulerAnglesX = cameraPivotRotate.localRotation.eulerAngles.x + GetMouseDeltaPos().y * _PrefConfig.RCSpeed;
            if (eulerAnglesX >= _minAngleAxisX && eulerAnglesX <= _maxAngleAxisX)
            {
                cameraPivotRotate.Rotate(new Vector3(GetMouseDeltaPos().y * _PrefConfig.RCSpeed, 0, 0));
            }
            cameraPivotTranslate.Rotate(new Vector3(0, GetMouseDeltaPos().x * _PrefConfig.RCSpeed, 0), Space.World);
        }

        if (Input.GetKey(_PrefConfig.RCUpward) || Input.GetKey(_PrefConfig.RCDownward))
        {
            if (Input.GetKey(_PrefConfig.RCUpward) && cameraPivotRotate.localRotation.eulerAngles.x - _rotateSpeed < _maxAngleAxisX)
            {
                cameraPivotRotate.Rotate(new Vector3(_rotateSpeed, 0, 0));
            }
            if (Input.GetKey(_PrefConfig.RCDownward) && cameraPivotRotate.localRotation.eulerAngles.x + _rotateSpeed > _minAngleAxisX)
            {
                cameraPivotRotate.Rotate(new Vector3(-_rotateSpeed, 0, 0));
            }
        }

        if (Input.GetKey(_PrefConfig.RCLeft))
        {
            cameraPivotTranslate.Rotate(new Vector3(0, _rotateSpeed, 0));
        }
        if (Input.GetKey(_PrefConfig.RCRight))
        {
            cameraPivotTranslate.Rotate(new Vector3(0, -_rotateSpeed, 0));
        }

        cameraPivotZoom.LookAt(cameraPivotTranslate);
    }
}

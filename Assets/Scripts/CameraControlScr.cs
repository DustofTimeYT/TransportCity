using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControlScr : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform cameraPivot;

    [Range(0.01f, 0.5f)]
    [SerializeField] private float _moveSpeed;

    [Range(1f, 10f)]
    [SerializeField] private float _cameraSpeed;

    private Vector3 _downClickMousePosition;
    private Vector3 _downClickCameraPosition;

    private Vector3 _deltaMousePosition;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cameraPivot.Translate(new Vector3(0f, 0f, _moveSpeed));
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraPivot.Translate(new Vector3(0f, 0f, -_moveSpeed));
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraPivot.Translate(new Vector3(-_moveSpeed, 0f, 0f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraPivot.Translate(new Vector3(_moveSpeed, 0f, 0f));
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            if (cameraPivot.position.y - Input.mouseScrollDelta.y > 0 && cameraPivot.position.y - Input.mouseScrollDelta.y < 20)
            {
                cameraPivot.Translate(new Vector3(0f, -Input.mouseScrollDelta.y, 0f));
            }
        }

        _camera.transform.position = Vector3.Lerp(_camera.transform.position, cameraPivot.position, Time.deltaTime * _cameraSpeed);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                _downClickMousePosition = Input.mousePosition;
                _downClickCameraPosition = cameraPivot.position;
            }

            _deltaMousePosition = _downClickMousePosition - Input.mousePosition;

            //cameraPivot.position = new Vector3(_downClickCameraPosition.x + _deltaMousePosition.x * _moveSpeed * 0.1f, cameraPivot.position.y, _downClickCameraPosition.z + _deltaMousePosition.y * _moveSpeed * 0.1f);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScr : MonoBehaviour
{
    public Transform cameraPivot;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cameraPivot.Translate(new Vector3(0f, 0f, 0.1f));
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraPivot.Translate(new Vector3(0f, 0f, -0.1f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraPivot.Translate(new Vector3(-0.1f, 0f, 0f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraPivot.Translate(new Vector3(0.1f, 0f, 0f));
        }
    }
}

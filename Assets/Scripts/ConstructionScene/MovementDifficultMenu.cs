using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDifficultMenu : MonoBehaviour
{
   

    void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(0 , transform.position.y - Camera.main.transform.position.y, 0) , Vector3.up);
    }
}

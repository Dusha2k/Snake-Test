using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float speed = 5f;
    void Update()
    {
        if (UIManager.Instance.fever)
            speed = 15f;
        else
            speed = 5f;
        
        transform.position = transform.position + transform.forward * (speed * Time.deltaTime);
    }
}

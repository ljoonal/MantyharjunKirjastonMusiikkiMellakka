using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 12.0f;
    public Vector3 offset;
    public int zoom;
    
    void Start()
    {
        offset[0] = -6.0f;
        offset[1] = 11.0f;
        offset[2] = 6.0f;
    }
    void Zoom()
    {
        offset[0] -= zoom;
        offset[1] += zoom;
        offset[2] += zoom;
        zoom = 0;
    }
    void FixedUpdate()
    {
        if(zoom != 0)
        {
            Zoom();
        }
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
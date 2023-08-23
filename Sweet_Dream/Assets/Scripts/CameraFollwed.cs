using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollwed : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 camera_offset;
    // Start is called before the first frame update
    void Start()
    {
        camera_offset = transform.position - target.position;         
    }

    private void FixedUpdate()
    {
        transform.position = target.position + camera_offset;
    }
}

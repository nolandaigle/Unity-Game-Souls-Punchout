using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    }
}

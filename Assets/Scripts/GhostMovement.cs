using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Vector3 ghostPosition;
    public float ghostSpeed;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += ghostPosition * ghostSpeed * Time.deltaTime;

        if (transform.position.z <= -50)
        {
            transform.position = initialPosition;
        }
    }
}

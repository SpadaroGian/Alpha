using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Vector3 ghostPosition;
    public float ghostSpeed;

    public float limitWall;
    public string salle;
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
        if (salle == "S2")
        {
            if (transform.position.x <= limitWall)
            {
                transform.position = initialPosition;
            }
        }
        if (salle == "S3")
        {
            if (transform.position.z <= limitWall)
            {
                transform.position = initialPosition;
            }
        }
    }
}

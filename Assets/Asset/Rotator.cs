using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0); // degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}

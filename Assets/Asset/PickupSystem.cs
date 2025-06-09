using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public float pickupRange = 5f;

    private GameObject heldObject;

    void Update()
    {
        bool pickupPressed = false;
        bool dropPressed = false;

        // Pickup keys/buttons
        if (Input.GetKeyDown(KeyCode.B))           // Keyboard B
            pickupPressed = true;

        if (Input.GetKeyDown(KeyCode.JoystickButton2)) // Controller X button
            pickupPressed = true;

        if (Input.GetKeyDown(KeyCode.JoystickButton0)) // Controller A button
            pickupPressed = true;

        // Drop keys/buttons
        if (Input.GetKeyDown(KeyCode.N))           // Keyboard N
            dropPressed = true;

        if (Input.GetKeyDown(KeyCode.JoystickButton3)) // Controller Y button
            dropPressed = true;

        if (heldObject == null)
        {
            GameObject closest = null;
            float closestDist = Mathf.Infinity;

            GameObject[] pickables = GameObject.FindGameObjectsWithTag("Pickable");

            foreach (GameObject obj in pickables)
            {
                float dist = Vector3.Distance(transform.position, obj.transform.position);
                if (dist < closestDist && dist <= pickupRange)
                {
                    closest = obj;
                    closestDist = dist;
                }
            }

            if (closest != null)
            {
                Debug.Log("Press B (keyboard) or X/A (controller) to pick up " + closest.name);

                if (pickupPressed)
                {
                    Debug.Log("Picking up " + closest.name);
                    heldObject = closest;
                    heldObject.transform.SetParent(transform);
                    heldObject.transform.localPosition = new Vector3(0, 1, 1);
                    Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                    if (rb)
                    {
                        rb.isKinematic = true;
                    }
                    // Remove the "Pickable" tag while holding
                    heldObject.tag = "Untagged";
                }
            }
        }
        else
        {
            if (dropPressed)
            {
                Debug.Log("Dropping " + heldObject.name);
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();

                heldObject.transform.SetParent(null);
                heldObject.transform.position = transform.position + transform.forward * 1.5f + Vector3.up * 0.5f;

                if (rb)
                {
                    rb.isKinematic = false;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.AddForce(transform.forward * 2f, ForceMode.VelocityChange);
                }

                // Restore the "Pickable" tag when dropped
                heldObject.tag = "Pickable";

                heldObject = null;
            }
        }
    }
}

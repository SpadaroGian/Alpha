using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objPickup : MonoBehaviour
{
    public Transform objTransform, cameraTrans;
    public bool interactable, pickedup;
    public Rigidbody objRigidbody;
    public float throwAmount;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (!pickedup)
            {
                interactable = false;
            }
            else
            {
                objTransform.parent = null;
                objRigidbody.useGravity = true;
                interactable = false;
                pickedup = false;
            }
        }
    }

    void Update()
    {
        if (interactable)
        {
            // Press A to pick up
            if (Input.GetKeyDown(KeyCode.A) && !pickedup)
            {
                objTransform.parent = cameraTrans;
                objRigidbody.useGravity = false;
                pickedup = true;
            }

            // Release A to drop
            if (Input.GetKeyUp(KeyCode.A) && pickedup)
            {
                objTransform.parent = null;
                objRigidbody.useGravity = true;
                pickedup = false;
            }

            // Press R to throw
            if (pickedup && Input.GetKeyDown(KeyCode.R))
            {
                objTransform.parent = null;
                objRigidbody.useGravity = true;
                objRigidbody.velocity = cameraTrans.forward * throwAmount * Time.deltaTime;
                pickedup = false;
            }
        }
    }
}

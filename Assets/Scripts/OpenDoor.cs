using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR;

public class OpenDoor : MonoBehaviour
{
    public Animator animator;

    private bool isClose = true;
    private bool canMove = true;

    public TMP_Text opendoor;

    private InputDevice device;
    public XRNode controllerNode = XRNode.RightHand; // Change to LeftHand if needed

    void Start()
    {
        opendoor.gameObject.SetActive(false);
        device = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    void Update()
    {
        // Refresh the device if not valid
        if (!device.isValid)
        {
            device = InputDevices.GetDeviceAtXRNode(controllerNode);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("RealPlayer"))
        {
            if (this.name == "doorhitbox1")
            {
                opendoor.gameObject.SetActive(true);
            }

            bool isPressed = false;
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out isPressed) && isPressed)
            {
                if (isClose && canMove)
                {
                    Debug.Log("Open");
                    canMove = false;
                    animator.SetTrigger("open");
                    isClose = false;
                    StartCoroutine(WaitDoor());
                }
                else if (!isClose && canMove)
                {
                    Debug.Log("Close");
                    canMove = false;
                    animator.SetTrigger("close");
                    isClose = true;
                    StartCoroutine(WaitDoor());
                }

                if (this.name == "r2text")
                {
                    Debug.Log("cccccccccccc");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RealPlayer"))
        {
            opendoor.gameObject.SetActive(false);
        }
    }

    private IEnumerator WaitDoor()
    {
        yield return new WaitForSeconds(1f);
        canMove = true;
    }
}

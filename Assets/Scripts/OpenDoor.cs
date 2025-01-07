using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    public Animator animator;

    private bool isClose = true;

    private bool canMove = true;

    public TMP_Text opendoor;
    // Start is called before the first frame update
    void Start()
    {
        opendoor.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("RealPlayer"))
        {
            //Debug.Log("player");
            if (this.name == "doorhitbox1")
            {
                opendoor.gameObject.SetActive(true);
            }
            if (Input.GetKey(KeyCode.E) && isClose && canMove)
            {
                Debug.Log("Open");
                canMove = false;
                animator.SetTrigger("open");
                isClose = false;
                StartCoroutine(WaitDoor());
                
            }
            if (Input.GetKey(KeyCode.E) && !isClose && canMove)
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

    private void OnTriggerExit(Collider other)
    {
        opendoor.gameObject.SetActive(false);
    }

    private IEnumerator WaitDoor()
    {
        yield return new WaitForSeconds(1f); // Adjust the delay time as needed
        canMove = true;
    }
}

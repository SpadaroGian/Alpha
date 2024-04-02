using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 5f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    bool portalEntered = false;

    public int playerhp = 3;

    public bool level1pass = false;

    public TMP_Text Endscreen;

    private void Start()
    {
        Endscreen.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            if (other.gameObject.name == "Portal R1S1S2")
            {
                Debug.Log("tp");
                this.transform.position = new Vector3(32.5f, 0f, 12.5f);
                portalEntered = true;

                StartCoroutine(ResetPortalEntered());
            }
            if (other.gameObject.name == "Portal R1S2S1")
            {
                Debug.Log("tp");
                this.transform.position = new Vector3(17.5f, 0f, 12.5f);
                portalEntered = true;

                StartCoroutine(ResetPortalEntered());
            }
            if (other.gameObject.name == "Portal R1S2S3")
            {
                Debug.Log("tp");
                this.transform.position = new Vector3(82.5f, 0f, -12.5f);
                portalEntered = true;

                StartCoroutine(ResetPortalEntered());
            }
            if (other.gameObject.name == "Portal R1S3S2")
            {
                Debug.Log("tp");
                this.transform.position = new Vector3(67.5f, 0f, -12.5f);
                portalEntered = true;

                StartCoroutine(ResetPortalEntered());
            }
            if (other.gameObject.name == "Portal R1R2" && level1pass)
            {
                Debug.Log("R2");
                this.transform.position = new Vector3(200f, 0f, -25f);
                portalEntered = true;

                Endscreen.gameObject.SetActive(true);

                StartCoroutine(ResetPortalEntered());


            }
            if (other.gameObject.name == "Portal R0R1")
            {
                Debug.Log("tp");
                this.transform.position = new Vector3(-17.5f, 0f, -17.5f);
                portalEntered = true;

                StartCoroutine(ResetPortalEntered());
            }
            if (other.gameObject.name == "Portal R1R0")
            {
                Debug.Log("tp1");
                this.transform.position = new Vector3(-42.5f, 0f, 2.5f);
                portalEntered = true;

                StartCoroutine(ResetPortalEntered());
            }
        }
        if (other.gameObject.tag == "Ghost")
        {
            Debug.Log("gh");
            this.playerhp -= 1;
            if (this.playerhp <= 0)
            {
                Object.Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator ResetPortalEntered()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay time as needed
        portalEntered = false;
    }

    void Update()
    {
        if (!portalEntered)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            //Debug.Log(this.transform.position);

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
}




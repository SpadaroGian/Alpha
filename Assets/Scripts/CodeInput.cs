using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CodeInput : MonoBehaviour
{
    public GameObject AskForCode;
    public TMP_InputField CodeEntered;
    public string correctCode = "1234";

    private void Start()
    {
        AskForCode.SetActive(false);
        CodeEntered.gameObject.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "RealPlayer")
        {
            string enteredCode = CodeEntered.text;

            CodeEntered.gameObject.SetActive(true);
            AskForCode.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("enter");
                if (enteredCode == correctCode)
                {
                    Debug.Log("correct");
                }
                else
                {
                    Debug.Log(enteredCode);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        CodeEntered.gameObject.SetActive(false);

        AskForCode.SetActive(false);
    }
}

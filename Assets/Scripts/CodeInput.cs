using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeInput : MonoBehaviour
{
    public TMP_Text AskForCode;
    public TMP_InputField CodeEntered;
    public string correctCode = "1234";

    private void Start()
    {
        AskForCode.gameObject.SetActive(false);
        CodeEntered.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RealPlayer")
        {
            AskForCode.text = "Press enter to submit code";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "RealPlayer")
        {

            CodeEntered.gameObject.SetActive(true);
            AskForCode.gameObject.SetActive(true);


            if (Input.GetKey(KeyCode.Return))
            {
                Debug.Log("enter");
                if (CodeEntered.text == correctCode)
                {
                    Debug.Log("correct");
                    AskForCode.text = "Correct";

                    Debug.Log("level1pass");
                    other.gameObject.GetComponent<PlayerMovement>().level1pass = true;
                    
                    Debug.Log(other.gameObject.GetComponent<PlayerMovement>().level1pass);
                }
                else
                {
                    //other.gameObject.GetComponent<PlayerMovement>().playerhp -= 1;
                    Debug.Log("incorrect");
                    AskForCode.text = "Wrong code";
                    CodeEntered.text = "";
                }
            }

            if (other.gameObject.GetComponent<PlayerMovement>().playerhp <= 0) 
            {
                CodeEntered.gameObject.SetActive(false);
                AskForCode.gameObject.SetActive(false);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        CodeEntered.gameObject.SetActive(false);
        AskForCode.gameObject.SetActive(false);
    }
}

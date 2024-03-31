using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string scenename;
    public string exitName;
    //public Vector3 arrival;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetString("LastExitName", exitName);
            SceneManager.LoadScene(scenename);
            //PlayerScript.instance.transform.position = arrival;
        }
    }
}
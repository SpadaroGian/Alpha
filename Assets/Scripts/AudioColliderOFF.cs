using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioColliderOFF : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RealPlayer")
        {
            audioSource.Stop();
        }
    }

}


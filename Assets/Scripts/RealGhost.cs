using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform targetObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (((Mathf.Abs(targetObj.position.x - this.transform.position.x) < 10) &&(Mathf.Abs(targetObj.position.z - this.transform.position.z) < 10)))
        {
            //Debug.Log("x diff " + (Mathf.Abs(targetObj.position.x - this.targetObj.position.x)));
            //Debug.Log("z diff " + (Mathf.Abs(targetObj.position.z - this.targetObj.position.z)));
            transform.position = Vector3.MoveTowards(this.transform.position, targetObj.position, 2 * Time.deltaTime);
        }
    }


}

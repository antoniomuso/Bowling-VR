using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    private Vector3 startingPos;
    private Quaternion startingRot;

    // Start is called before the first frame update
    void Start() {
        startingPos = this.transform.position;
        startingRot = this.transform.rotation;

        Debug.Log(startingPos);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void resetObject() {
        this.transform.position = startingPos;
        this.transform.rotation = startingRot;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}

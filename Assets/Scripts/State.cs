using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    private Vector3 startingPos;
    private Quaternion startingRot;
    public GameObject spawn;
    public float force;

    // Start is called before the first frame update
    void Start() {
        if (spawn != null) {
            startingPos = spawn.transform.position;
            startingRot = spawn.transform.rotation;
        }
        else {
            startingPos = this.transform.position;
            startingRot = this.transform.rotation;
        }

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

        if (force != 0)
            this.GetComponent<Rigidbody>().AddForce(0, 0, -force, ForceMode.Impulse);
    }
}

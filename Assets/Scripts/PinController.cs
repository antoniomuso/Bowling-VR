using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PinController : MonoBehaviour
{
    public GameObject[] pins;
    private List<GameObject> notFallen;

    public List<Vector3> startingPos;
    public List<Quaternion> startingRot;


    // Start is called before the first frame update
    void Start()
    {
        notFallen = new List<GameObject>(pins);
        foreach (GameObject pin in pins) {
            startingPos.Add(pin.transform.position);
            startingRot.Add(pin.transform.rotation);
        }
        //initialize round and tiri
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        notFallen.Remove(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + "on trigger enter"); 
    }

    public int GetPoints(){
        return pins.Length - notFallen.Count;
    }


    public void upliftNotFallenPins(Callback cb)
    {
        
    }


    public void resetPositions() {
        for (int i = 0; i < pins.Length; i++) {
            pins[i].transform.position = startingPos[i];
            pins[i].transform.rotation = startingRot[i];
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    

    /*Nuovo
    public void resetPositions()
    {
        for (int i = 0; i < notFallen.Count; i++)
        {
            notFallen[i].transform.position = startingPos[i];
            notFallen[i].transform.rotation = startingRot[i];
            notFallen[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            notFallen[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
    */
}

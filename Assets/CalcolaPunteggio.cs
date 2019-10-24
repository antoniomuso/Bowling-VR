using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CalcolaPunteggio : MonoBehaviour
{
    public GameObject[] birilli;
    private List<GameObject> fallen;

    public List<Vector3> startingPos;
    public List<Quaternion> startingRot;


    // Start is called before the first frame update
    void Start()
    {
        fallen = new List<GameObject>(birilli);
        foreach (GameObject pin in birilli) {
            startingPos.Add(pin.transform.position);
            startingRot.Add(pin.transform.rotation);
        }
            

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        fallen.Remove(other.gameObject);
        Debug.Log("Punteggio " + (birilli.Length - fallen.Count));
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + "on trigger enter"); 
    }

    public int GetPoints(){
        return fallen.Count;
    }

    public void resetPositions(){
        for (int i = 0; i < birilli.Length; i++) {
            birilli[i].transform.position = startingPos[i];
            birilli[i].transform.rotation = startingRot[i];
            birilli[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            birilli[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}

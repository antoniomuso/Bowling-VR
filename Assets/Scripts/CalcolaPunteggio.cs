using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CalcolaPunteggio : MonoBehaviour
{
    public GameObject[] birilli;
    private List<GameObject> notFallen;
    int round;
    int tiroCorrente;
    int tiriTotali;
    int[] punteggioTiro;
    public List<Vector3> startingPos;
    public List<Quaternion> startingRot;


    // Start is called before the first frame update
    void Start()
    {
        notFallen = new List<GameObject>(birilli);
        foreach (GameObject pin in birilli) {
            startingPos.Add(pin.transform.position);
            startingRot.Add(pin.transform.rotation);
        }
        //initialize round and tiri
        round = 1;
        tiriTotali = 10;
        tiroCorrente = 0;
        punteggioTiro = new int[10];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Numero round: "+round+" di Tiro "+(tiroCorrente+1)+"\n"
                +"Punteggio: "+(birilli.Length-notFallen.Count));
        }
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
        return birilli.Length - notFallen.Count;
    }


    public void resetPositions(){
        for (int i = 0; i < birilli.Length; i++) {
            birilli[i].transform.position = startingPos[i];
            birilli[i].transform.rotation = startingRot[i];
            birilli[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            birilli[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
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

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CalcolaPunteggio : MonoBehaviour
{
    public GameObject[] birilli;
    private List<GameObject> fallen;
        
    // Start is called before the first frame update
    void Start()
    {
        fallen = new List<GameObject>(birilli);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Punteggio " + (10 - fallen.Count));
    }

    private void OnTriggerExit(Collider other)
    {
        fallen.Remove(other.gameObject);
        Debug.Log(other.gameObject.name + " on trigger stay");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + "on trigger enter"); 
    }
}

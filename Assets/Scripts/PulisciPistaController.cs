using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulisciPistaController : MonoBehaviour
{
    public bool attivaPulisciPista;
    // Start is called before the first frame update
    void Start()
    {
        attivaPulisciPista = false;
        //this.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attivaPulisciPista)
            attiva();
    }

    private void attiva()
    {
        //this.GetComponent<Renderer>().enabled = true;
        Debug.Log("Pulizia pista in corso...");
        attivaPulisciPista = false;
    }

}

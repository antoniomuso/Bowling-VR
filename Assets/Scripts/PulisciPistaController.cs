using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PulisciPistaController : MonoBehaviour
{
    private Animator anim;
    private Callback cb;

    // Start is called before the first frame update
    void Start(){
        anim = GetComponent<Animator> (); 
        
        //this.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attiva(Callback cb)
    {
        //this.GetComponent<Renderer>().enabled = true;
        Debug.Log("Pulizia pista in corso...");
        anim.SetBool("isActive", true);
        this.cb = cb;
        
    }

    void AnimationEnded (string message) {  
        Debug.Log("messaggio: " +  message);
        if (message.Equals("AnimEnded")) {
            anim.SetBool("isActive", false);
            cb();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public delegate void callback(); // declare delegate type


public class PulisciPistaController : MonoBehaviour
{
    public Animator anim;
    private callback cb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();
        
        
        //this.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attiva(callback cb)
    {
        //this.GetComponent<Renderer>().enabled = true;
        Debug.Log("Pulizia pista in corso...");
        anim.SetBool("isActive", true);
        this.cb = cb;
        
    }

    void AnimationEnded (string message) {  
        if (message.Equals("AnimEnded")) {
            anim.SetBool("isActive", false);
            cb();
        }
    }

}

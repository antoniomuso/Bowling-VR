using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PulisciPistaController : MonoBehaviour
{
    private Animator anim;
    private Callback cb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        this.cb = () => { };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attiva(Callback cb)
    {
        Debug.Log("Pulizia pista in corso...");
        this.cb = cb;
        anim.SetBool("isActive", true);
    }

    void AnimationEnded(string message)
    {
        Debug.Log("messaggio: " + message);
        if (message.Equals("AnimEnded"))
        {
            anim.SetBool("isActive", false);
            this.cb();
        }
    }

}

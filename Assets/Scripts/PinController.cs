using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class PinController : MonoBehaviour
{
    public GameObject[] pins;
    private HashSet<GameObject> notFallen;


    // Start is called before the first frame update
    void Start()
    {
        resetNotFallen();

        foreach (var pin in pins)
        {
            var anim = pin.GetComponent<Animation>();

            foreach (AnimationState state in anim) state.speed = 0.3F;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        notFallen.Remove(other.gameObject);
        //Debug.Log(other.gameObject.name + "on trigger exit"); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pins.Contains(other.gameObject))
        {
            notFallen.Add(other.gameObject);
            //Debug.Log(other.gameObject.name + "on trigger enter"); 
        }
    }

    public int GetPoints()
    {
        return pins.Length - notFallen.Count;
    }


    public void upliftNotFallenPins()
    {
        foreach (var pin in notFallen)
        {
            pin.GetComponent<State>().resetObject();
            pin.GetComponent<Animation>().Play();
        }
    }

    private void resetNotFallen()
    {
        notFallen = new HashSet<GameObject>(pins);
    }

    public void resetPositions()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].GetComponent<State>().resetObject();
        }
        resetNotFallen();
    }
}

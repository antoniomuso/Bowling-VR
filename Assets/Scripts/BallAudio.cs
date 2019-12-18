using System;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    public GameObject ball;
    public GameObject floor;
    public GameObject[] pins;
    private AudioSource ball_audio;
    private AudioSource pin_audio;
    
    
    void Start()
    {
        ball_audio = ball.GetComponent<AudioSource>();
        pin_audio = pins[0].GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(floor))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            ball_audio.Play();
        }
    }


    void OnCollisionStay(Collision collision)
    {
        if (Array.Exists(pins, element => element == collision.gameObject))
        {
            pin_audio.Play();
        }
    }
}

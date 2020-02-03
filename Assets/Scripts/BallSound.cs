using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour
{
    public GameObject ball;
    public GameObject ball_1;
    public GameObject ball_2;
    public GameObject floor;
    public GameObject[] pins;
    private AudioSource[] ball_audio;
    private AudioSource pin_audio;
    private Rigidbody rb;


    void Start()
    {
        ball_audio = ball.GetComponents<AudioSource>();
        pin_audio = pins[0].GetComponent<AudioSource>();
        rb = ball.GetComponent<Rigidbody>();
        //for (int i = 0; i < 10; i++)
        //{
        //    pin_audio[i] = pins[i].GetComponent<AudioSource>();
        //}
    }


    // Update is called once per frame
    void Update()
    {

    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.Equals(floor))
    //    {
    //        //source.volume = collision.relativeVelocity.magnitude / maxforce
    //        ball_audio.Play();
    //    }
    //}


    //void OnCollisionStay(Collision collision)
    //{
    //    if (Array.Exists(pins, element => element == collision.gameObject))
    //    {
    //        pin_audio.Play();
    //    }
    //}

    void OnCollisionEnter(Collision collision)
    {
        //if (Array.Exists(pins, element => element == collision.gameObject))
        //{
        //    pin_audio[0].Play();
        //    //for (int i = 0; i < 10; i++)
        //    //{

        //    //}
        //}
        if (collision.gameObject.Equals(pins[0]))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            pin_audio.Play();
        }
        if (collision.gameObject.Equals(pins[1]))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            pin_audio.Play();
        }
        if (collision.gameObject.Equals(pins[2]))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            pin_audio.Play();
        }
        if (collision.gameObject.Equals(pins[3]))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            pin_audio.Play();
        }
        if (collision.gameObject.Equals(pins[4]))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            pin_audio.Play();
        }
        if (collision.gameObject.Equals(pins[5]))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            pin_audio.Play();
        }
        if (collision.gameObject.Equals(pins[6]))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            pin_audio.Play();
        }
        if (collision.gameObject.Equals(pins[7]))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            pin_audio.Play();
        }
        if (collision.gameObject.Equals(pins[8]))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            pin_audio.Play();
        }
        if (collision.gameObject.Equals(pins[9]))
        {
            //source.volume = collision.relativeVelocity.magnitude / maxforce
            pin_audio.Play();
        }
        if (collision.gameObject.Equals(floor))
        {
            //ball_audio.volume = collision.relativeVelocity.magnitude / maxforce;
            if (rb.velocity.magnitude > 1.0)
            {
                Debug.Log(rb.velocity.magnitude);
                ball_audio[0].Play();
            }
            else
                ball_audio[1].Play();
            
        }
        if ((collision.gameObject.Equals(ball_1) || collision.gameObject.Equals(ball_2)) && rb.velocity.magnitude > 0.1)
        {
            Debug.Log(rb.velocity.magnitude);
            ball_audio[2].Play();
        }
    }
}

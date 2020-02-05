using UnityEngine;
using System;

public class BallSound : MonoBehaviour
{
    public GameObject floor;
    private AudioSource[] ball_audio;
    private Rigidbody rb;


    void Start()
    {
        ball_audio = this.GetComponents<AudioSource>();
        rb = this.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(floor))
        {
            ball_audio[1].volume = Mathf.Clamp01(fun(collision.relativeVelocity.magnitude));
            //Debug.Log("Velocity: " + collision.relativeVelocity.magnitude);
            //Debug.Log("Volume:" + ball_audio[1].volume);
            ball_audio[1].Play();
        }
        if ((collision.gameObject.tag == "Ball") && rb.velocity.magnitude > 0.1)
        {
            ball_audio[2].volume = Mathf.Clamp01(fun(collision.relativeVelocity.magnitude));
            ball_audio[2].Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.Equals(floor))
        {
            if (rb.velocity.magnitude > 0.1 && !ball_audio[1].isPlaying && !ball_audio[0].isPlaying)
                ball_audio[0].Play();
            

            if (rb.velocity.magnitude < 0.1 && ball_audio[0].isPlaying)
                ball_audio[0].Stop();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.Equals(floor) && ball_audio[0].isPlaying)
            ball_audio[0].Stop();
    }

    public static float fun(double value)
    {
        if (value <= 0) return (float)0.01;
        else return (float)Math.Exp(-1 / value);
    }

}

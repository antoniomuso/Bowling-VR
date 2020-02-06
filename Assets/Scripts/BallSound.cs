using UnityEngine;
using System;

public class BallSound : MonoBehaviour
{
    public GameObject floor;
    private AudioSource[] ball_audio;


    void Start()
    {
        ball_audio = this.GetComponents<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(floor))
        {
            ball_audio[1].volume = Mathf.Clamp01(fun(collision.relativeVelocity.magnitude));
            //Debug.Log("Velocity: " + collision.relativeVelocity.magnitude);
            //Debug.Log("Volume:" + ball_audio[1].volume);
            ball_audio[1].Play();
        }
        else if ((collision.gameObject.tag != "Pin"))
        {
            ball_audio[2].volume = Mathf.Clamp01(fun(collision.relativeVelocity.magnitude));
            ball_audio[2].Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!ball_audio[0].isPlaying)
            ball_audio[0].Play();

        if (ball_audio[0].isPlaying)
            ball_audio[0].volume = Mathf.Clamp01(fun(collision.relativeVelocity.magnitude));
    }

    private void OnCollisionExit(Collision collision)
    {
        if (ball_audio[0].isPlaying)
            ball_audio[0].Stop();
    }

    public static float fun(double value)
    {
        if (value <= 0) return 0.001f;
        else return (float) Math.Exp(-1 / value);
    }
}

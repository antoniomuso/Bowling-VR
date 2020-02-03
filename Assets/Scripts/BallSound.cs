using UnityEngine;
using System;

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
    }


    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision collision)
    {

        if (Array.Exists(pins, element => element == collision.gameObject))
        {
            //pin_audio.volume = Mathf.Clamp01(collision.relativeVelocity.magnitude);
            pin_audio.Play();
        }

        if (collision.gameObject.Equals(floor))
        {
            //ball_audio[0].volume = Mathf.Clamp01(rb.velocity.magnitude; /// 20); //più o meno velocità massima?
            if (rb.velocity.magnitude > 1.0)
            {
                Debug.Log(rb.velocity.magnitude);
                //ball_audio[0].volume = Mathf.Clamp01(rb.velocity.magnitude); /// 20); //più o meno velocità massima?
                ball_audio[0].Play();
            }
            else
            {
                //ball_audio[1].volume = Mathf.Clamp01(collision.relativeVelocity.magnitude);
                ball_audio[1].Play();
            }
                
            
        }
        if ((collision.gameObject.Equals(ball_1) || collision.gameObject.Equals(ball_2)) && rb.velocity.magnitude > 0.1)
        {
            Debug.Log(rb.velocity.magnitude);
            ball_audio[2].Play();
        }
    }
}

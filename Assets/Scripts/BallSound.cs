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
            //ball_audio[0].volume = Mathf.Clamp01(rb.velocity.magnitude; /// 20); //più o meno velocità massima?
            if (rb.velocity.magnitude > 1.0)
            {
                Debug.Log(rb.velocity.magnitude);
                //ball_audio[0].volume = Mathf.Clamp01(rb.velocity.magnitude); /// 20); //più o meno velocità massima?
                ball_audio[1].Play();
                while (ball_audio[1].isPlaying) { }
                ball_audio[0].Play();
            }
            else
            {
                //ball_audio[1].volume = Mathf.Clamp01(collision.relativeVelocity.magnitude);
                ball_audio[1].Play();
            }
                
            
        }
        if ((collision.gameObject.tag == "Ball") && rb.velocity.magnitude > 0.1)
        {
            Debug.Log(rb.velocity.magnitude);
            ball_audio[2].Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.Equals(floor))
        {
            if (rb.velocity.magnitude == 0)
            {
                ball_audio[0].Stop();
            }
        }
    }
}

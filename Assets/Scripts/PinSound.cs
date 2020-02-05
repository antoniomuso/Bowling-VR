using System;
using UnityEngine;

public class PinSound : MonoBehaviour
{
    public GameObject barra;
    private AudioSource pin_audio;

    // Start is called before the first frame update
    void Start()
    {
        pin_audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Pin" || collision.gameObject.Equals(barra))
        {
            pin_audio.volume = Mathf.Clamp01(BallSound.fun(collision.relativeVelocity.magnitude));
            //Debug.Log("Velocity: " + collision.relativeVelocity.magnitude);
            //Debug.Log("Volume:" + pin_audio.volume);
            pin_audio.Play();
        }
            
    }

}

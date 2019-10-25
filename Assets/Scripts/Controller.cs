using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject ball;
    bool launched;
    // Start is called before the first frame update
    void Start()
    {
        launched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched)
        {
            /*check bounds
            if (ball.transform.position.x >= 2.50)
                ball.position.x = 2.50;
            */
            if (ball.transform.localPosition.x >= -2.50 && Input.GetKey(KeyCode.A))
                ball.transform.Translate(-0.1f, 0, 0);
            if (ball.transform.localPosition.x <= 2.50 && Input.GetKey(KeyCode.D))
                ball.transform.Translate(0.1f, 0, 0);
            if (Input.GetKey(KeyCode.Space))
                launchBall();
        }

    }

    void launchBall()
    {
        ball.GetComponent<ConstantForce>().enabled = true;
        ball.GetComponent<Rigidbody>().useGravity = true;
    }
}

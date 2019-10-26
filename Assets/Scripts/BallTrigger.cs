using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    public GameObject ball;
    public GameObject pinController;
    public GameObject pulisciPista;

    //public List<int> throws;
    //public uint nThrows;
    

    //coordinates of the ball respawn
    public Vector3 startingPos;
    public Quaternion startingRot;

    // Start is called before the first frame update
    void Start(){
        //startingPos = ball.transform.position;
    }

    // Update is called once per frame
    void Update(){
        
    }

    private IEnumerator waiter()
    {
        yield return new WaitForSeconds(2);

        //throws.Add(pinController.GetComponent<CalcolaPunteggio>().GetPoints());

        //attiva pulisci pista
        pulisciPista.GetComponent<PulisciPistaController>().attiva( () => {
             resetBall();
             pinController.GetComponent<CalcolaPunteggio>().resetPositions();
        });

       

        /*
        if (nThrows >= throws.Count)
            throw new System.Exception("Too much throws!");
            */
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(ball)){
            StartCoroutine(waiter());
        }
    }

    public void resetBall() {
        ball.GetComponent<ConstantForce>().enabled = false;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.transform.position = startingPos;
        ball.transform.rotation = startingRot;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

}

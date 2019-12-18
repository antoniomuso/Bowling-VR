using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    public GameObject ball;
    public GameObject pinController;
    public GameObject pulisciPista;
    public Controller ControllerManager;

    private AudioSource audio1;
    public GameObject floor;
    //private AudioSource audio2;
    public int waiterSeconds = 2;

    public int numberOfRounds;
    int round;
    private List<(int first, int second)> roundsPoints;
    


    // Start is called before the first frame update
    void Start(){
        //var sources = GetComponents<AudioSource>();
        //audio1 = sources[0];
        //audio2 = sources[1];
        audio1 = GetComponent<AudioSource>();
        round = 0;
        roundsPoints = new List<(int first, int second)>();
        for (int i = 0; i < numberOfRounds; i++) {
            roundsPoints.Add((-1, -1));
        }
    }

    // Update is called once per frame
    void Update(){
    }

    private IEnumerator waiter() {
        yield return new WaitForSeconds(waiterSeconds);
        int points = pinController.GetComponent<PinController>().GetPoints();

        // If is the first throw 
        Debug.Log("Number of Point: " + points);
        if (roundsPoints[round].Equals((-1,-1))) {
            roundsPoints[round] = (points, -1);

        } else {
            roundsPoints[round] = (roundsPoints[round].first, points - roundsPoints[round].first);
            if (round < numberOfRounds) round ++;
        }


        //attiva pulisci pista
        pulisciPista.GetComponent<PulisciPistaController>().attiva(() => {

            resetBall();
            
            pinController.GetComponent<PinController>().resetPositions();
            Debug.Log(round);
        });

       

      
      
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audio1.Play();
        }
        


    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(ball)){
            StartCoroutine(waiter());
        }
    }


 

    public void resetBall() {
        ControllerManager.launched = true;
        ball.GetComponent<ConstantForce>().enabled = false;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

}

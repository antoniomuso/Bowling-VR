using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject pinController;
    public GameObject pulisciPista;

    private IntCallback cb;
    private bool cleanAll;

    // Start is called before the first frame update
    void Start() {
        StartThrow(true, true, (int score) => {
            Debug.Log("StrokeManager score: " + score);
        });
    }

    // Update is called once per frame
    void Update() {
    }

    private IEnumerator waiter()
    {
        yield return new WaitForSeconds(2);
        int points = pinController.GetComponent<PinController>().GetPoints();

        //ANIMAZIONE CHE ALZA I BIRILLI

        //attiva pulisci pista
        pulisciPista.GetComponent<PulisciPistaController>().attiva(() => {

            resetBall();
            pinController.GetComponent<PinController>().resetPositions();
            //cb(points);
        });

        //ANIMAZIONE CHE ABBASSA I BIRILLI
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(ball)) {
            StartCoroutine(waiter());
        }
    }

    public void resetBall()
    {
        ball.GetComponent<ConstantForce>().enabled = false;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<State>().resetObject();
    }

    public void StartThrow(bool reset, bool cleanAll, IntCallback cb) {
        this.cb = cb;
        this.cleanAll = cleanAll;

        //SETUP DEL TIRO
        if (reset){
            //ANIMAZIONE SET UP DEL TIRO
        }
    }
}

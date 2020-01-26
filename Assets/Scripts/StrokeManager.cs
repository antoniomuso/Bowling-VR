using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class StrokeManager : MonoBehaviour
{
    private GameObject ball;
    public GameObject pinController;
    public GameObject pulisciPista;

    private IntCallback cb;
    private bool cleanAll;
    private bool isConsecutiveCall = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
    }

    private IEnumerator waiter()
    {
        yield return new WaitForSeconds(5);
        isConsecutiveCall = false;
        int points = pinController.GetComponent<PinController>().GetPoints();
        Debug.Log("Punti Punti: " + points);

        //ANIMAZIONE CHE ALZA I BIRILLI

        //attiva pulisci pista
        if (!cleanAll) {
            pinController.GetComponent<PinController>().upliftNotFallenPins();
        }

        pulisciPista.GetComponent<PulisciPistaController>().attiva(() => {
            Debug.Log("Chiamata alla callback");
            resetBall();

            cb(points);
        });

        //ANIMAZIONE CHE ABBASSA I BIRILLI
    }

    IEnumerator WaitAndRespawn(Collider other)
    {
        yield return new WaitForSeconds(5);
        other.GetComponent<State>().resetObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball") {
            if (!isConsecutiveCall) {
                isConsecutiveCall = true;
                this.ball = other.gameObject;
                StartCoroutine(waiter());
            } else {
                StartCoroutine("WaitAndRespawn", other);
            }
        }
    }

    public GameObject getBall() {
        return this.ball;
    }

    public void resetBall()
    {
        getBall().GetComponent<State>().resetObject();
    }

    public Task<int> StartThrow(bool reset, bool cleanAll) { 
        TaskCompletionSource<int> task = new TaskCompletionSource<int>();
        this.cb = (int score) => task.TrySetResult(score); 
        this.cleanAll = cleanAll;


        //SETUP DEL TIRO
        if (reset){
            // Debug.Log("RESET BIRILLI !!");
            pinController.GetComponent<PinController>().resetPositions();
            
        }

        return task.Task;
    }
}

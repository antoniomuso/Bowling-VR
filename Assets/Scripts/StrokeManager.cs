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

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
    }

    private IEnumerator waiter()
    {
        yield return new WaitForSeconds(5);
        int points = pinController.GetComponent<PinController>().GetPoints();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball") {
            ball = other.gameObject;
            StartCoroutine(waiter());
        }
    }

    public void resetBall()
    {
        ball.GetComponent<State>().resetObject();
    }

    public Task<int> StartThrow(bool reset, bool cleanAll) { 
        TaskCompletionSource<int> task = new TaskCompletionSource<int>();
        this.cb = (int score) => task.TrySetResult(score); 
        this.cleanAll = cleanAll;


        //SETUP DEL TIRO
        if (reset){
            Debug.Log("RESET BIRILLI !!");
            pinController.GetComponent<PinController>().resetPositions();
            
        }

        return task.Task;
    }
}

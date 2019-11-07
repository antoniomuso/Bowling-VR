using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class StrokeManager : MonoBehaviour
{
    public GameObject ball;
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
        yield return new WaitForSeconds(2);
        int points = pinController.GetComponent<PinController>().GetPoints();

        //ANIMAZIONE CHE ALZA I BIRILLI

        //attiva pulisci pista
        pulisciPista.GetComponent<PulisciPistaController>().attiva(() => {

            resetBall();
            pinController.GetComponent<PinController>().resetPositions();
            Debug.Log(points);
            cb(points);
            Debug.Log("Ciao" + points);


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
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<State>().resetObject();
    }

    public Task<int> StartThrow(bool reset, bool cleanAll) { 
        TaskCompletionSource<int> task = new TaskCompletionSource<int>();
        this.cb = (int score) => task.TrySetResult(score); 
        this.cleanAll = cleanAll;


        //SETUP DEL TIRO
        if (reset){
            //ANIMAZIONE SET UP DEL TIRO
        }

        Debug.Log("PRIMA DEL RETURN");
        return task.Task;
    }
}

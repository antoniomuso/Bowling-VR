using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    public GameObject ball;
    public GameObject pinController;

    public List<int> throws;
    public uint nThrows;

    public Vector3 startingPos;

    // Start is called before the first frame update
    void Start(){
        startingPos = ball.transform.position;
    }

    // Update is called once per frame
    void Update(){
        
    }

    private IEnumerator waiter()
    {
        yield return new WaitForSeconds(2);
        throws.Add(pinController.GetComponent<CalcolaPunteggio>().GetPoints());

        ball.transform.position = startingPos;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        pinController.GetComponent<CalcolaPunteggio>().resetPositions();

        if (nThrows >= throws.Count)
            throw new System.Exception("Too much throws!");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(ball)){
            StartCoroutine(waiter());
      
        }
    }

    public void reset() {

    }

}

using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaTrigger : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitAndRespawn(Collider other)
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        other.GetComponent<State>().resetObject();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("onTriggerExit");
            StartCoroutine("WaitAndRespawn", other);
        }
    }
}

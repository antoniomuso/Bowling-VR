using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class FrameManager : MonoBehaviour
{
    public GameObject pinController;
    public GameObject strokeManager;

    //La fotocamera è una merda

    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
    }


    public async Task<List<int>> RunFrame(bool isLastFrame)
    {
        List<int> scores = new List<int>();
        int PinNumber = pinController.GetComponent<PinController>().pins.Length;

        if (!isLastFrame) {
            int score = await strokeManager.GetComponent<StrokeManager>().StartThrow(true, false);
            Debug.Log("Punti: " + score);
            scores.Add(score);
            if (score == PinNumber) {
                Debug.Log("Strike;");
                return scores;
            }

            //SECONDO TIRO
            score = await strokeManager.GetComponent<StrokeManager>().StartThrow(false, true);
            Debug.Log("Punti: " + score);
            scores.Add(score);

        } else {
            int score = await strokeManager.GetComponent<StrokeManager>().StartThrow(true, false);
            scores.Add(score);
            if (score == PinNumber) {

                //SECONDO TIRO
                score = await strokeManager.GetComponent<StrokeManager>().StartThrow(true, false);
                scores.Add(score);

                bool reset = (score == PinNumber);

                //TERZO TIRO
                score = await strokeManager.GetComponent<StrokeManager>().StartThrow(reset, true);
                scores.Add(score);

            } else {
                //SECONDO TIRO
                int secondScore = await strokeManager.GetComponent<StrokeManager>().StartThrow(false, true);
                scores.Add(secondScore);

                int TotalScore = score + secondScore;
                if (TotalScore == PinNumber) {
                    //TERZO TIRO
                    score = await strokeManager.GetComponent<StrokeManager>().StartThrow(true, true);
                    scores.Add(score);
                }
            }
        }
        Debug.Log("Ritornando i risultati");
        foreach (int i in scores) Debug.Log(i);
        return scores;
    }
}

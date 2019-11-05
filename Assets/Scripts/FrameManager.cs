using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrameManager : MonoBehaviour
{
    public GameObject pinController;
    public GameObject strokeManager;


    private ListIntCallback cb;
    private bool isLastFrame;

    //La fotocamera è una merda

    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
    }


    public void RunFrame(bool isLastFrame, ListIntCallback cb)
    {
        List<int> scores = new List<int>();
        int PinNumber = pinController.GetComponent<PinController>().pins.Length;

        if (!isLastFrame) {
            strokeManager.GetComponent<StrokeManager>().StartThrow(true, false, (int FirstScore) =>
            {
                scores.Add(FirstScore);
                if (FirstScore == PinNumber){
                    cb(scores);
                    return;
                }

                //SECONDO TIRO
                strokeManager.GetComponent<StrokeManager>().StartThrow(false, true, (int SecondScore) =>{
                    scores.Add(SecondScore);
                    cb(scores);
                });
            });
        } else {
            strokeManager.GetComponent<StrokeManager>().StartThrow(true, false, (int FirstScore) => {
                scores.Add(FirstScore);
                if (FirstScore == PinNumber) {
                    cb(scores);

                    //SECONDO TIRO
                    strokeManager.GetComponent<StrokeManager>().StartThrow(true, false, (int SecondScore) =>{
                        scores.Add(SecondScore);
                        cb(scores);

                        bool reset = (SecondScore == PinNumber);

                        //TERZO TIRO
                        strokeManager.GetComponent<StrokeManager>().StartThrow(reset, true, (int ThirdScore) => {
                            scores.Add(ThirdScore);
                            cb(scores);
                        });
                    });
                } else {
                    //SECONDO TIRO
                    strokeManager.GetComponent<StrokeManager>().StartThrow(false, true, (int SecondScore) => {
                        scores.Add(SecondScore);
                        cb(scores);

                        int TotalScore = FirstScore + SecondScore;
                        if (TotalScore == PinNumber) {
                            //TERZO TIRO
                            strokeManager.GetComponent<StrokeManager>().StartThrow(true, true, (int ThirdScore) => {
                                scores.Add(ThirdScore);
                                cb(scores);
                            });
                        }
                    });
                }
            });
        }
    }
}

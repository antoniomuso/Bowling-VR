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
        // ANIMAZIONE SETTARE 20 BIRILLI
        List<int> scores = new List<int>();
        int PinNumber = pinController.GetComponent<PinController>().pins.Length;

        //PRIMO TIRO
        strokeManager.GetComponent<StrokeManager>().StartThrow(false, (int FirstScore) => {
            scores.Add(FirstScore);
            if (FirstScore == PinNumber && !isLastFrame){
                cb(scores);
                return;
            }

            if (FirstScore == PinNumber){
                // RIALZA TUTTI I BIRILLI
                // RITIRA
            }

            //SECONDO TIRO
            strokeManager.GetComponent<StrokeManager>().StartThrow(true, (int SecondScore) =>
            {
                scores.Add(SecondScore);
                cb(scores);

                if (SecondScore == PinNumber && isLastFrame)
                    {
                        //RIALZA TUTTI I BIRILLI

                    } 
            });


          });

        //SECONDO TIRO


    }


}

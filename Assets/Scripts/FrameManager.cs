using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FrameManager : MonoBehaviour
{
    public GameObject pinController;
    public GameObject strokeManager;

    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
    }


    public async Task<List<int>> RunFrame(bool isLastFrame, int player, int frame)
    {
        List<int> scores = new List<int>();
        int PinNumber = pinController.GetComponent<PinController>().pins.Length;

        if (!isLastFrame) {
            int score = await strokeManager.GetComponent<StrokeManager>().StartThrow(true, false);
            Debug.Log("Punti: " + score);
            scores.Add(score);
            if (score == PinNumber) {
                ScoresUI.instance.SetScore(player, frame, 0, "X");
                Debug.Log("Strike;");
                return scores;
            }

            ScoresUI.instance.SetScore(player, frame, 0, score.ToString());
            

            //SECONDO TIRO
            score = await strokeManager.GetComponent<StrokeManager>().StartThrow(false, true);
            Debug.Log("Punti: " + score);
            scores.Add(score);
            int sub = (scores[1] - scores[0]);

            if (score == PinNumber) ScoresUI.instance.SetScore(player, frame, 1, "/");
            else ScoresUI.instance.SetScore(player, frame, 1, sub.ToString());

        } else {
            int score = await strokeManager.GetComponent<StrokeManager>().StartThrow(true, false);
            scores.Add(score);
            if (score == PinNumber) {
                ScoresUI.instance.SetScore(player, frame, 0, "X");
                //SECONDO TIRO
                score = await strokeManager.GetComponent<StrokeManager>().StartThrow(true, false);
                scores.Add(score);
                
                bool reset = (score == PinNumber);

                if (reset) ScoresUI.instance.SetScore(player, frame, 1, "X"); 
                else ScoresUI.instance.SetScore(player, frame, 1, (scores[1] - scores[0]).ToString()); 

                //TERZO TIRO
                score = await strokeManager.GetComponent<StrokeManager>().StartThrow(reset, true);
                scores.Add(score);
                
                if (score == PinNumber && scores[1] == PinNumber) ScoresUI.instance.SetScore(player, frame, 2, "X"); 
                else if (score == PinNumber) ScoresUI.instance.SetScore(player, frame, 2, "/"); 
                else ScoresUI.instance.SetScore(player, frame, 2, (scores[2] - scores[1]).ToString()); 

            } else {
                ScoresUI.instance.SetScore(player, frame, 0, score.ToString()); 
                //SECONDO TIRO
                int secondScore = await strokeManager.GetComponent<StrokeManager>().StartThrow(false, true);
                scores.Add(secondScore);

                int TotalScore = score + secondScore;
                if (TotalScore == PinNumber) {
                    ScoresUI.instance.SetScore(player, frame, 1, "/"); 

                    //TERZO TIRO
                    score = await strokeManager.GetComponent<StrokeManager>().StartThrow(true, true);
                    scores.Add(score);

                    if (score == PinNumber) ScoresUI.instance.SetScore(player, frame, 2, "X"); 
                    else ScoresUI.instance.SetScore(player, frame, 2, (scores[2] - scores[1]).ToString()); 
                }
            }
        }
        Debug.Log("Ritornando i risultati");
        foreach (int i in scores) Debug.Log(i);
        return scores;
    }
}

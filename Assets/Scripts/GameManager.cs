using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public delegate void Callback();
public delegate void IntCallback(int number);
public delegate void ListIntCallback(List<int> number);


public class GameManager : MonoBehaviour {
    public GameObject frameManager;

    public int playersNumber;
    public int totalFrames;

    // player -> frame -> punteggio
    private List<List<List<int>>> gameScores;

    private int[] totScore;
    private int[] timesRedoubling;

    // Start is called before the first frame update
    void Start() {  
        totScore = new int[playersNumber];
        timesRedoubling = new int[playersNumber];

        gameScores = new List<List<List<int>>>();
        for (int i = 0; i < playersNumber; i++) {
            List<List<int>> point = new List<List<int>>();
            for(int j = 0; j < totalFrames; j++) {
                point.Add(new List<int>());
            }
            gameScores.Add(point);
        }
        StartGame();
    }

    private async void StartGame() {
        ScoresUI.instance.SetNumberOfPlayers(playersNumber);
        
        for (int f = 0; f < totalFrames; f++)
        {
            Debug.Log("Frame: " + f);
            for (int p = 0; p < playersNumber; p++)
            {
                Debug.Log("Player: " + p);
                List<int> score = await frameManager.GetComponent<FrameManager>().RunFrame(f == totalFrames, p, f);
                Debug.Log("Frame: " + f + " Player: " + p + " -> " );

                // Questo è lo score dove per ogni tiro hai quanti birilli hai buttato esattamente in quel tiro
                int[] scoreT = new int[score.Count];

                // Creo l'array
                scoreT[0] = score[0];
                for (int i = 1; i < scoreT.Length; i++) {
                    scoreT[i] = score[i] - score[i-1]; 
                }

                // Setto i punteggi nella lista dei punteggi 
                gameScores[p][f] = new List<int>(scoreT);

                int sum = 0;
                // Raddoppio del punteggio
                for (int i = 0; i < score.Count && timesRedoubling[p] > 0; i++, timesRedoubling[p]--) {
                    // raddoppio il tiro i
                    continue;
                    sum += score[i];
                }

                if (score.Count == 1 && score[0] == 10) {
                    // Abbiamo fatto strike
                    timesRedoubling[p] += 2;   

                } else if (score.Count == 2 && score[1] == 10) {
                    // Abbiamo fatto spare
                    timesRedoubling[p] ++;

                }
                
                // Qua va fatta la somma.

                // Aggiorno graficamente 
                ScoresUI.instance.setParzialScore(p, f,  score[score.Count - 1].ToString());

                
            }
        }
    }
    // Update is called once per frame
    void Update() {

    }

}



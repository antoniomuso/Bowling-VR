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
    private List<List<List<int>>> gameScores;

    // Start is called before the first frame update
    void Start() {  
        gameScores = new List<List<List<int>>>();
        for (int i = 0; i < playersNumber; i++)
            gameScores.Add(new List<List<int>>());
        StartGame();
    }

    private async void StartGame() {
        for (int f = 0; f < totalFrames; f++)
        {
            Debug.Log("Frame: " + f);
            for (int p = 0; p < playersNumber; p++)
            {
                Debug.Log("Player: " + p);
                List<int> score = await frameManager.GetComponent<FrameManager>().RunFrame(f == totalFrames);
                Debug.Log("Frame: " + f + " Player: " + p + " -> " );
                foreach (int i in score) Debug.Log(i);
            }
        }
    }
    // Update is called once per frame
    void Update() {

    }

}



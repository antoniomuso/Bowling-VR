using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Callback();
public delegate void IntCallback(int number);
public delegate void ListIntCallback(List<int> number);


public class GameManager : MonoBehaviour
{
    public GameObject frameManager;
    public int playersNumber;
    public int totalFrames;
    private List<List<List<int>>> gameScores;

    private int currentPlayer;
    private int currentFrame;
        
    // Start is called before the first frame update
    void Start() {
        gameScores = new List<List<List<int>>>();
        for (int i = 0; i< playersNumber; i++)
            gameScores.Add(new List<List<int>>());

   }

    // Update is called once per frame
    void Update() {
        
    }

    private IEnumerator waiter(Callback cb) {
        yield return new List<int>(); 
    }
}


